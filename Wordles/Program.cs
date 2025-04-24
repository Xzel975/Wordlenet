using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Spectre.Console;

namespace Wordle
{

    class Program
    {
        public static int nivelActual = 0;
        
            public static void Main(string[] args)
        {
           
           
            Word w = new Word();
           w.addPalabras();
           string name = "";
           int nivel = 0;
           
           if (File.Exists("savedates.txt"))
{
    string[] temporal = File.ReadAllLines("savedates.txt");

    if (temporal.Length >= 2) // Validamos que haya al menos dos líneas
    {
        name = temporal[0];
        Int32.TryParse(temporal[1], out nivel);
    }
    else
    {
        AnsiConsole.WriteLine("[red]El archivo de guardado está corrupto o incompleto. Se generará un nuevo perfil.[/red]");
        generateProfile();
        string[] nuevoTemporal = File.ReadAllLines("savedates.txt");
        name = nuevoTemporal[0];
        Int32.TryParse(nuevoTemporal[1], out nivel);
    }
}
else
{
    generateProfile();
    string[] temporal = File.ReadAllLines("savedates.txt");
    name = temporal[0];
    Int32.TryParse(temporal[1], out nivel);
}
            User u = new User(name,nivel);

            menu(u,w);
        
        

           
           
              
            
            }
                
              
            
       public static void saveDates(User u)
        {

        
        try
        {
            string[] temporalDates = new string[]
        {
            u.name ?? string.Empty,
            u.nivel.ToString() ?? "0"
        };

             File.WriteAllLines("savedates.txt",temporalDates);   
               
               
           
        AnsiConsole.WriteLine("[green]Datos actualizados correctamente en el archivo de guardado.[/green]");
         }
    catch 
    {
        
        AnsiConsole.WriteLine($"[red]Error al actualizar los datos[/red]");
    }

        }
        
        

       public static void menu(User u,Word w)
        {
            string opcionesMenu = AnsiConsole.Prompt(
    new SelectionPrompt<string>()

        .PageSize(10)
        
        .AddChoices(new[] {
            "Jugar","Ver Perfil","Creditos","Salir"
        }));
       
       switch (opcionesMenu)
       {
            case "Jugar":
            Game(u,w);
            break;

            case "Ver Perfil":
            viewProfile(u,w);
            break;

            case "Salir":

            break;



       }

        }

        static void closeGame(User u)
        {
            saveDates(u);

            AnsiConsole.Status() .Start("Cargando", ctx =>
            {
                     ctx.Status("Cerrando Juego");
        ctx.Spinner(Spinner.Known.Grenade);
        ctx.SpinnerStyle(Style.Parse("blue"));
         Thread.Sleep(1000);

            });

            Environment.Exit(0);

        }

         static void generateProfile()
        {

            //Aca se genera un pefil para el usuario en base al nombre que elija
            AnsiConsole.WriteLine("Bienvenido a Wordle",Color.BlueViolet);
            Thread.Sleep(1000);
            AnsiConsole.WriteLine(@"A continuacion ingresa tu nombre [Lo podras cambiar despues]",Color.Aquamarine1);
            bool confirmacionNombre = false;
            string Name = "";
            while(confirmacionNombre == false)
            {
                 string selectname = AnsiConsole.Prompt(new TextPrompt<string>("..."));
                var confirmation = AnsiConsole.Prompt(
    new TextPrompt<bool>("Esta seguro?")
        .AddChoice(true)
        .AddChoice(false)
        .DefaultValue(true)
        .WithConverter(choice => choice ? "y" : "n"));

        if(confirmation == true){Name = selectname; confirmacionNombre = true;}
        else{AnsiConsole.WriteLine("[white]Ingresa de nuevo el nombre[/]");}

            }
            Console.Clear();
            AnsiConsole.WriteLine($"[green]Perfecto tu perfil ha sido creado con el nombre[/]{Name}");
          


           try
        {
              // Creamos el archivo y escribimos los datos directamente
       string[] temporalDates = new string[]
        {
            Name ?? string.Empty,
            nivelActual.ToString()
        };

            File.WriteAllLines("savedates.txt",temporalDates);
        AnsiConsole.WriteLine("[green]Archivo de guardado creado.[/green]");
         }
    catch 
    {
        
        AnsiConsole.WriteLine($"[red]Error al crear el archivo de guardado[/red]");
    }
            
      
            
            
        }

        
        static void viewProfile(User u,Word w)
        {
            
            
     Table profile = new Table();

     profile.AddColumn("[blue]Profile[/]");
     profile.AddRow($"Nombre{u.name}");
    profile.AddRow($"Nivel{u.nivel}");

     profile.Border(TableBorder.Heavy);

     AnsiConsole.Write(profile);
  
string select = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Que desea hacer")
        .PageSize(10)
        .AddChoices(new[] {
            "Cambiar Nombre","Eliminar Datos","Volver al menu"

        }));

        switch (select)
        {
            case "Cambiar Nombre":
            changeName(u);
            break;
            case "Eliminar Datos":
            deleteSave();
            break;
            case "Volver al menu":
            menu(u,w);
            break;

        }
        


        }

        static void changeName(User u)
        {
           AnsiConsole.WriteLine("[blue]Ingresa el nuevo nombre[/]");
           bool confirmacionNombre = false;
           string newName = "";
             while(confirmacionNombre == false)
            {
                 string selectname = AnsiConsole.Prompt(new TextPrompt<string>("..."));
                var confirmation = AnsiConsole.Prompt(
    new TextPrompt<bool>($"[white]Estas Seguro del nombre? : {selectname}")
        .AddChoice(true)
        .AddChoice(false)
        .DefaultValue(true)
        .WithConverter(choice => choice ? "y" : "n"));

        if(confirmation == true){newName = selectname; confirmacionNombre = true;}
        else{AnsiConsole.WriteLine("[white]Ingresa de nuevo el nombre[/]");}

            }

            u.name = newName;
            saveDates(u);

        }

        static void deleteSave()
        {
            try
            {
                if(File.Exists("savedates.txt"))
                {
                    File.Delete("savedates.txt");
                    AnsiConsole.Status() .Start("Por favor espere", ctx=> 
                    {
                          ctx.Status("Borrando archivo de guardado");
        ctx.Spinner(Spinner.Known.Star);
        ctx.SpinnerStyle(Style.Parse("blue"));
            Thread.Sleep(2000);
            AnsiConsole.MarkupLine("[green]Archvo Borrado con exito✅[/green]");
            AnsiConsole.MarkupLine("[red]Se cerrar la app en breve[/red]");
            Thread.Sleep(2000);


                    });

                    
                }
                else{Environment.Exit(0);}

            }
            catch
            {
                AnsiConsole.WriteLine("[red]ERROR INESPERADO NO PUEDE BORRAR EL ARCHIVO[/]");
                Console.ReadKey();
                Environment.Exit(0);

            }
        }

        
        
        static void Game(User u,Word w)
        {
            string n = u.nivel.ToString();


            

            Table tabla = Word.GenerationLayout(u);
           
          
         AnsiConsole.Write(tabla);

         Word.updateTable(tabla, u);

        
        }

       

    }
  

}
