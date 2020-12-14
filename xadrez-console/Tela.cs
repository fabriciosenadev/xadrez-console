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
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- "); // posição vazia
                    }
                    else
                    {
                        Console.Write(tab.peca(i, j) + " "); // posicação com peça
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
