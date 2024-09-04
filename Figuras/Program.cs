using System;
using Shared;
using System.IO;
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Figuras
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Declaracion de variables listas, paths, serializador etc zzzz
            List<ObjectFiguras> lista = new List<ObjectFiguras>();
            
            string opcion;
            string[] tipos={"Circulo","Rectangulo","Triangulo"};
            int L1, L2, L3, id=1;

            string xmlpath = Combine(CurrentDirectory, "lista.xml"); //le digo a el xml con q voy a trabajar y el directorio donde guardarse
            var xs= new XmlSerializer(typeof(List<ObjectFiguras>));
            #endregion

            Console.WriteLine("Eliga el numero de la opcion deseada"); //menu principal
            Console.WriteLine("1-Crear figuras");
            Console.WriteLine("2-Obtener area");
            opcion=Console.ReadLine();
            switch(opcion){
                case "1":
                    while(opcion!="0"){
                    Console.WriteLine("¿Que figura desea crear?");    //menu de creacion de figuras
                    Console.WriteLine("1-Circulo");
                    Console.WriteLine("2-Rectangulo");
                    Console.WriteLine("3-Triangulo");
                    Console.WriteLine("INGRESE 0 PARA DEJAR DE CREAR FIGURAR");
                    opcion=Console.ReadLine();
                    switch(opcion){
                        case "1":
                           Console.WriteLine("Ingrese el valor del radio del circulo"); 
                           L1=Convert.ToInt32(Console.ReadLine());
                           lista.Add(new ObjectFiguras(id, L1));
                           id++;
                        break;

                        case "2":
                           Console.WriteLine("Ingrese el valor de la base del rectangulo");      //Segun que figura se cree se usa un constructor que le asigna su clasificacion por defecto
                           L1=Convert.ToInt32(Console.ReadLine()); 
                           Console.WriteLine("Ingrese el valor de la altura del rectangulo");
                           L2=Convert.ToInt32(Console.ReadLine());
                           lista.Add(new ObjectFiguras(id, L1, L2));
                           id++;
                        break;

                        case "3":
                           Console.WriteLine("Ingrese el valor del lado 1 del triangulo");
                           L1=Convert.ToInt32(Console.ReadLine());
                           Console.WriteLine("Ingrese el valor del lado 2 del triangulo");
                           L2=Convert.ToInt32(Console.ReadLine());
                           Console.WriteLine("Ingrese el valor del lado 3 del triangulo");
                           L3=Convert.ToInt32(Console.ReadLine());
                           lista.Add(new ObjectFiguras(id,L1, L2, L3));
                           id++;
                        break;
                    } 
                    Console.Clear();  //limpio pa q no c vea tan feo
                    }

                    #region serializacion
                    using (FileStream Stream=File.Create(xmlpath)){     //serializacion :v
                        xs.Serialize(Stream, lista);
                    }
                    #endregion

                break;

                case "2":
                    #region deserializacion
                    using (FileStream xmlLoad = File.Open(xmlpath, FileMode.Open)){               
                    var loadedfiguras = (List<ObjectFiguras>)xs.Deserialize(xmlLoad);
                        foreach (var item in loadedfiguras){
                            if(item.L2.Equals(0)){
                                lista.Add(new ObjectFiguras(item.id, item.L1));
                                
                            }else{
                                if(item.L3.Equals(0)){
                                    lista.Add(new ObjectFiguras(item.id, item.L1, item.L2));    //deserealizacion usando los mismos constructores para clasificar que tipo de figura es cada uno
                                    
                                }else{
                                    lista.Add(new ObjectFiguras(item.id, item.L1, item.L2, item.L3));
                                    
                                }
                            }
                        }
                    }
                    #endregion

                    WriteLine("Seleccione como quiere buscar la(s) figura a calcular"); //menu de las opciones de filtrado para las areas
                    Console.WriteLine("1-Todas las figuras");
                    Console.WriteLine("2-Por tipo");
                    Console.WriteLine("3-Por indice (empezando desde el 0)");
                    opcion=Console.ReadLine();

                    switch(opcion){
                        case "1":
                        foreach (var item in lista){
                            WriteLine($"{item.Clasificacion+" "+item.subclasificacion} numero {item.id} con un area {area(item.Clasificacion, item.L1, item.L2, item.L3)}");  //imprimir todas xd
                        }
                        break;

                        case "2":
                            Console.WriteLine("¿Que tipo quieres?");
                            Console.WriteLine("1-Circulo");
                            Console.WriteLine("2-Rectangulo");
                            Console.WriteLine("3-Triangulo");
                            opcion=Console.ReadLine();
                            
                            foreach(var item in lista){
                                if(item.Clasificacion.Equals(tipos[int.Parse(opcion)-1])){
                                    WriteLine($"{item.Clasificacion+" "+item.subclasificacion} numero {item.id} con un area {area(item.Clasificacion, item.L1, item.L2, item.L3)}");// imprimir por tipo/clasificacion usando un string de referencia
                                }
                            }
                        break;

                        case "3":
                            Console.WriteLine("Ingrese el id de la figura empezando por el 1: ");
                            opcion=Console.ReadLine();
                            foreach(var item in lista){
                                if(item.id.Equals(int.Parse(opcion))){
                                    WriteLine($"{item.Clasificacion+" "+item.subclasificacion} numero {item.id} con un area {area(item.Clasificacion, item.L1, item.L2, item.L3)}");//imprimir por id/indice/index/idk ez
                                }
                            }
                        break;
                    }
                break;
            }
        }
        #region funcion de calculo de area
        static string area(string clas, int lado1, int lado2, int lado3){ //funcion para calcular el area usando la clasificacion/tipo como ref
            double a, s;
            switch(clas){
                case "Circulo":
                a=Math.PI*(lado1^2);
                return a.ToString();

                case "Rectangulo":
                a=lado1*lado2;
                return a.ToString();
                
                case "Triangulo":
                s=lado1+lado2+lado3;
                a=Math.Sqrt(s*(s-lado1)*(s-lado2)*(s-lado3)); //utilizo el teorema de heron para el calculo del area para no preocuparme por el tipo de triangulo
                return a.ToString();
            }
            return "sin esta madre me marca error pero nunca lo va a terminar devolviendo esto xd"; //honestamente podria quitar esto y poner un return solo para todos pero ya tengo mucho sueño y me da weba
        }
        #endregion
    }
}