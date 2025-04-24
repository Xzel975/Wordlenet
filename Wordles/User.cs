using System.Collections.Generic;

namespace Wordle
{

    class User
    {
          /*

          Se crean las variables del usuario estas son:
          Nombre
          Nivel
          */  
       
        public  string? name;
        public  int nivel = 1;
        
       public static Dictionary<string,string> bag = new Dictionary<string, string>();



        public User(string name,int nivel)
        {

            this.name = name;
            this.nivel = nivel;
            


        }

    }



}
