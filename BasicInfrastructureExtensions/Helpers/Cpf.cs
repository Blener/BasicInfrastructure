using System;
using BasicInfrastructureExtensions.Extensions;

namespace BasicInfrastructureExtensions.Helpers
{

    public class Cpf
    {
        private static string GerarDigito(string cpf)
        {
            var peso = 2;
            var soma = 0;

            for (var i = cpf.Length - 1; i >= 0; i--)
            {
                soma += peso*Convert.ToInt32(cpf[i].ToString());
                peso++;
            }

            var pNumero = 11 - (soma%11);

            if (pNumero > 9)
                pNumero = 0;

            return pNumero.ToString();
        }

        public static bool Validar(string cpf)
        {
            // Se for vazio
            if (cpf == null || cpf.IsEmpty())
                return false;

            // Retirar todos os caracteres que não sejam numéricos
            var aux = cpf.ExtractNumbers();

            // O tamanho do CPF tem que ser 11
            if (aux.Length != 11)
                return false;

            for (var number = 0; number < 10; number++)
            {
                if (aux == new String(number.ToString()[0], 11))
                    return false;
            }

            // Guardo o dígito para comparar no final
            var pDigito = aux.Substring(9, 2);
            aux = aux.Substring(0, 9);

            //Cálculo do 1o. digito do CPF
            aux += GerarDigito(aux);

            //Cálculo do 2o. digito do CPF
            aux += GerarDigito(aux);

            return pDigito == aux.Substring(9, 2);
        }

        public static string Generate()
        {
            var r = new Random();
            int a1 = r.Next(10),
                a2 = r.Next(10),
                a3 = r.Next(10),
                a4 = r.Next(10),
                a5 = r.Next(10),
                a6 = r.Next(10),
                a7 = r.Next(10),
                a8 = r.Next(10),
                a9 = r.Next(10);
            int b1 = a1*10,
                b2 = a2*9,
                b3 = a3*8,
                b4 = a4*7,
                b5 = a5*6,
                b6 = a6*5,
                b7 = a7*4,
                b8 = a8*3,
                b9 = a9*2;
            var dv1 = b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9;
            var t2 = dv1%11; // Cálculo do resto da divisão
            int primeiroDigitoVerificador;
            if (t2 <= 2)
                primeiroDigitoVerificador = 0;
            else
                primeiroDigitoVerificador = 11 - t2;

            int c1 = a1*11,
                c2 = a2*10,
                c3 = a3*9,
                c4 = a4*8,
                c5 = a5*7,
                c6 = a6*6,
                c7 = a7*5,
                c8 = a8*4,
                c9 = a9*3,
                c10 = primeiroDigitoVerificador*2;
            var dv2 = c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9 + c10;
            var t4 = dv2%11; // Cálculo do resto da divisão
            int segundoDigitoVerificador;
            if (t4 <= 2)
                segundoDigitoVerificador = 0;
            else
                segundoDigitoVerificador = 11 - t4;
            return string.Format("{0}{1}{2}.{3}{4}{5}.{6}{7}{8}-{9}{10}", a1, a2, a3, a4, a5, a6, a7, a8, a9, primeiroDigitoVerificador, segundoDigitoVerificador);
        }
    }
}