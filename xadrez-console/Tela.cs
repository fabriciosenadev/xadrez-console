using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida) // realiza todas as impressões da partida na tela
        {
            imprimirTabuleiro(partida.tab);

            Console.WriteLine();

            imprimirPecasCapturadas(partida);

            Console.WriteLine();

            Console.WriteLine("Turno: " + partida.turno);
            Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

            if(partida.xeque) // verifica que a jogada coloca o adversário em xeque
            {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida) // imprime as peças capturadas separadas por conjunto de cores
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            
            Console.WriteLine();
            
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor; // pega a cor original
            Console.ForegroundColor = ConsoleColor.Yellow; // imprime as pretas
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux; // retoma a cor original
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto) // imprime o conjunto das peças que foram capturadas
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");

        }

        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            // imprime o tabuleiro na tela
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " "); // numeros na lateral esquerda do tabuleiro
                for (int j = 0; j < tab.colunas; j++)
                {
                    imprimirPeca(tab.peca(i, j)); // posicação com peça para ser impressa
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h"); // letras da base do tabuleiro
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis) // imprime também as posições possíveis
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            // imprime o tabuleiro na tela
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " "); // numeros na lateral esquerda do tabuleiro
                for (int j = 0; j < tab.colunas; j++)
                {
                    if(posicoesPossiveis[i, j]) // exibe a posição em destaque quando a casa for um movimento possível
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else // caso não seja um movimento possível exibe com fundo preto
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j)); // posicação com peça para ser impressa
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h"); // letras da base do tabuleiro
            Console.BackgroundColor = fundoOriginal; // reforça o retorno do estádo original da cor de fundo
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        { // lê do teclado o que o usuário digitar
            string s = Console.ReadLine(); // usuário irá informar uma posição no tabuleiro ex: c2
            char coluna = s[0]; // pega a letra da coluna digitada
            int linha = int.Parse(s[1].ToString()); // pega o número da linha digitada
            return new PosicaoXadrez(coluna, linha); // retorna uma nova posição no tabuleiro
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null) // se não existir peça 
            {
                Console.Write("- "); // posição vazia
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca); // peça branca
                }
                else
                {
                    // imprime a peça da cor diferente de branca
                    ConsoleColor aux = Console.ForegroundColor; // guarda a cor atual das letras do console
                    Console.ForegroundColor = ConsoleColor.Yellow; // altera a cor da letra para amarelo
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
