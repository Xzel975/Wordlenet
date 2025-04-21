using System.Collections.Generic;

namespace Wordle
{

    class User
    {
          /*

          Se crean las variables del usuario estas son:
          Nombre
          Nivel
          Dinero
          Inventario con objects(TAL VEZ)
          */  
       
        public  string? name;
        public  int nivel = 1;
        public  int money = 0;
       public static Dictionary<string,string> bag = new Dictionary<string, string>();



        public User(string name,int nivel,int money )
        {

            this.name = name;
            this.nivel = nivel;
            this.money = money;


        }

    }



}
