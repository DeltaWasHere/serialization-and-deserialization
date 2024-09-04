using System;


namespace Shared
{
    public class ObjectFiguras
    {
        public string Clasificacion;

        public string subclasificacion;

        public int id;
        public int L1; //Lo dejo como L1 para no confundirme en las otras figuras pero bien peude representar el rado del circulo xd
        public int L2;
        public int L3;

        public ObjectFiguras(){ //Constructor del circulo
            
        }
        public ObjectFiguras(int idd, int Lado1){ //Constructor del circulo
            id=idd;
            Clasificacion="Circulo";
            L1=Lado1;
            subclasificacion="";
        }

        public ObjectFiguras(int idd, int Lado1, int Lado2){ //Constructor del rectangulo
            id=idd;
            Clasificacion="Rectangulo";
            L1=Lado1;
            L2=Lado2;
            subclasificacion="";
        }

        public ObjectFiguras(int idd, int Lado1, int Lado2, int Lado3){ //Constructor del triangulo
            id=idd;
            Clasificacion="Triangulo";
            L1=Lado1;
            L2=Lado2;
            L3=Lado3;
            if(Lado1==Lado2&&Lado2==Lado1){
                subclasificacion="equilatero";
            }
            if(Lado1==Lado2||Lado2==Lado3||Lado1==Lado3){
                subclasificacion="isoceles";
            }
            if(Lado1!=Lado2 && Lado2!=Lado3 && Lado3!=Lado1){
                subclasificacion="escaleno";
            }
        }

    }
}