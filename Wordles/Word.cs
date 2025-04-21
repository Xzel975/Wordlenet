using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Spectre.Console;

namespace Wordle 
{
        class Word 
    {

public static List<string> Palabras { get; set; } = new List<string>();
public static Table juego = new Table();

//Se usan la variable x para saber en que posicion se encuentra cada palabra y Y para saber en que fila se esta
private static int Y = 0;
private static int X = 0;

        public  void addPalabras()
        {
            
        //Aqui se añaden las palabras del archivo de texto a una lista de palabras

        //El try de aca intenta ver si existe el archivo y en caso de que exista añadira las palabras a la lista
      try
      {
        if(File.Exists("list.txt") == true)
        {
            foreach (string linea in File.ReadLines("list.txt"))
            {
                Palabras.Add(linea);
            }

        AnsiConsole.Status() .Start("Cargando juego",ctx =>
        {

                   ctx.Status("Inicializando Juego");
        ctx.Spinner(Spinner.Known.Star);
        ctx.SpinnerStyle(Style.Parse("blue"));
            Thread.Sleep(2000);
            AnsiConsole.MarkupLine("[green]Carga Completa[/green]");
            Thread.Sleep(2000);

        }
        
        );


        }
    }
    catch
    {
   if(File.Exists("list.txt") == false)
   {Console.WriteLine("Archivo no encontrado");
   }
            

    }
         

          
        
    }

    public static Table GenerationLayout(User u)
    {
       
 
    //Se crea la tabla del wordle

           Table game = generationTable(Palabras[u.nivel]);
    //Se crea la tabla del menu
            Table menu = new Table();

            menu.AddColumn($"[green]Nivel:{u.nivel}[/]");
            menu.AddRow(game);
            menu.AddRow("Ingresa La palabra");
            menu.Border(TableBorder.Ascii);
           
           juego = game;
             return menu;

    }

    public static Table generationTable(string palabra)
    {
    //Se generan las variables x , la cual tiene el numero de columnas

            int x = 0;
    
 
    //Se crea la tabla

   

            var tabla = new Table();

            foreach(char letra in palabra)
            {
                 string[] wordle = new string[]{"w","o","r","d","l","e","s","","C","#"};
                      tabla.AddColumn(wordle[x]);
                x ++;

                
                 
            }

var rowValues = Enumerable.Repeat("______", x).ToArray();  
        //Se crean las filas por defecto son 5

     for(int i = 5; i > 0; i --)
     {
tabla.AddRow(rowValues);
     }
    
    
        


        return tabla;
    }

 public static void updateTable(Table a,User u)
    {

        
        Table updateTable = a;

        int recorrido = 5;
        string select = "";

//Aqui se le preguntara la palabra al usuario y en caso de tener la misma cantidad de Caracteres 

        while(recorrido > 0)
        {
         select = AnsiConsole.Prompt(new TextPrompt<string>("..."));
    if(select.Length == Word.Palabras[u.nivel].Length)
    {
        comprobarPalabra(Palabras[u.nivel],u);

        AnsiConsole.Write(juego);
        
        if(select == Palabras[u.nivel])
        {
            
            Win();

        }
        else{

                recorrido --;
                Y ++;


        }
        

    }

    else
    {
        AnsiConsole.WriteLine("[red]La palabra es muy pequeña o excede el limite[/]");
    }
        }


        if(select != Palabras[u.nivel])
        {
            Lose();
        }
        

       
    }


    private static void comprobarPalabra(string palabra,User u)
    {

        //Lista con las letras de la palabra del usuario
        List<char> posicionPalabra = new List<char>{};
        //Lista con las posiciones de la palabra del nivel
        List<char> nivelPalabras = new List<char>{};
        //Lista con la posicion x
        List<int> positionX = new List<int>{};
        

        foreach(char letra in palabra)
        {
            posicionPalabra.Add(letra);
        }
        foreach(char letra in Palabras[u.nivel])
        {
            nivelPalabras.Add(letra);
        }

        //Ahora se comprueba si la posicion de la letra coincide con la que dio el usuario
        


        //En caso de que la palabra este ene sa posicion se pondra la palabra de color Verde

        for(int x = 0; x < palabra.Length; x++)
        {
                if(nivelPalabras[x] == posicionPalabra[x])
                {
                    positionX.Add(posicionPalabra[x]);

                  juego.UpdateCell(Y, x, new Text($"{posicionPalabra[X]}",new Style(Color.Green)));
       
                }
                else if(nivelPalabras[x] != posicionPalabra[x])
                {
                    positionX.Add(posicionPalabra[x]);

                  juego.UpdateCell(Y, x, new Text($"{posicionPalabra[X]}",new Style(Color.Red)));
       
                }


        }


   
}

    public static void Win()
    {
        Table win = new Table();
        Console.Clear();
        win.AddColumn("");
        win.AddRow(new Panel(new Text("Ganaste",new Style(Color.Gold1))));
        AnsiConsole.Write(win);

    }
    public static void Lose()
    {

Table win = new Table();
        Console.Clear();
        win.AddColumn("");
        win.AddRow(new Panel(new Text("Lose",new Style(Color.Gold1))));
         AnsiConsole.Write(win);
    }


}


}