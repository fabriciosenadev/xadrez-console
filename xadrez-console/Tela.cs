using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            // imprime o tabuleiro na tela
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " "); // numeros na lateral esquerda do tabuleiro
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- "); // posição vazia
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i, j)); // posicação com peça para ser impressa
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h"); // letras da base do tabuleiro
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                // imprime a peça da cor diferente de branca
                ConsoleColor aux = Console.ForegroundColor; // guarda a cor atual das letras do console
                Console.ForegroundColor = ConsoleColor.Yellow; // altera a cor da letra para amarelo
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
