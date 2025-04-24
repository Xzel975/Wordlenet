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
public static Table nivel = new Table();

private static string select {get; set;} = "Ingrese una palabra";


private static int Y = 0;


        public  void addPalabras()
        {
            

      try
      {
        if(File.Exists("list.txt") == true)
        {
            foreach (string linea in File.ReadLines("list.txt"))
            {
                Palabras.Add(linea.ToLower());
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
       
 
    

            nivel = generationTable(u);
    
           

            juego.AddColumn($"[green]juego:{u.nivel}[/]");
            juego.AddRow(nivel);
            juego.AddRow($"{select}");
            juego.Border(TableBorder.Ascii);
           
           
             
            return juego;
    }

    public static Table generationTable(User u)
    {
             int x = 0;
            var tableWordle = new Table();

            foreach(char letra in Palabras[u.nivel])
            {
                     
                 string[] wordle = new string[]{"w","o","r","d","l","e","s","","C","#"};
                      tableWordle.AddColumn(wordle[x]);
                x ++;

                
                 
            }

var rowValues = Enumerable.Repeat("______", x).ToArray();  
        

     for(int i = 5; i > 0; i --)
     {
tableWordle.AddRow(rowValues);
     }
    
   

        return tableWordle;
    }

 public static void updateTable(Table a, User u)
{
    Table updateTable = a;
    int recorrido = 5;

   
    while (recorrido > 0)
    {
        select = AnsiConsole.Prompt(new TextPrompt<string>("..."));
        if (select.Length == Word.Palabras[u.nivel].Length)
        {
            comprobarPalabra(select.ToLower(), u);
            Console.Clear();
           
            juego = new Table();
            juego.AddColumn($"[green]Nivel:{u.nivel}[/]");
            juego.AddRow(nivel);
            juego.AddRow($"{select}");
            juego.Border(TableBorder.Ascii);

            AnsiConsole.Write(juego);

            if (select == Palabras[u.nivel])
            {
                Win(u);
                break;
            }
            else
            {
                recorrido--;
                Y++;
            }
        }
        else
        {
            AnsiConsole.WriteLine("[red]La palabra es muy pequeña o excede el límite[/]");
        }
    }

    if (select != Palabras[u.nivel])
    {
        Lose(u);
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

                  nivel.UpdateCell(Y, x, new Text($"{posicionPalabra[x]}",new Style(Color.Green)));
       
       //En caso de estar letra en la palabra pero no en esa posición se pone de color amarillo
                }
                else if(nivelPalabras.Contains(posicionPalabra[x]))
                {
                    positionX.Add(posicionPalabra[x]);

                  nivel.UpdateCell(Y, x, new Text($"{posicionPalabra[x]}",new Style(Color.Yellow)));
       
                }
            //Y en caso de NO estar la letra se pone de color blanco
                else
                {
                     nivel.UpdateCell(Y, x, new Text($"{posicionPalabra[x]}",new Style(Color.White)));
                }


        }


   
}

    public static void Win(User u)
    {
        Table win = new Table();
        Console.Clear();
        win.AddColumn("");
        win.AddRow(new Panel(new Text("Ganaste",new Style(Color.Gold1))));
        u.nivel ++;
        AnsiConsole.Write(win);
        Thread.Sleep(2000);
        Word w = new Word();
        Program.saveDates(u);
        Program.menu(u,w);

    }
    public static void Lose(User u)
    {

Table lose = new Table();
        Console.Clear();
        lose.AddColumn("");
        lose.AddRow(new Panel(new Text("Lose",new Style(Color.Gold1))));
         AnsiConsole.Write(lose);
         Thread.Sleep(2000);
         Word w = new Word();
        Program.menu(u,w);
    }


}


}