using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Spectre.Console;

namespace Wordle
{

    class Program
    {
        public static int nivelActual = 1;
            public static void Main(string[] args)
        {
           //Se crean las intancias y la lecture del archivo de texto
            Word w = new Word();
           w.addPalabras();
            User u = new User("Manuel",1,100);
        

            
            Game(u,w);
        }

        
        static void Game(User u,Word w)
        {
            string n = u.nivel.ToString();


            //Se crea la tabla

            Table tabla = Word.GenerationLayout(u);
           
          
         AnsiConsole.Write(tabla);

         Word.updateTable(tabla, u);

        
        }

       

    }
  

}
