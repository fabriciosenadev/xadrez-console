using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null; // posição inical da peça
            this.cor = cor;
            this.tab = tab;
            qteMovimentos = 0; // movimentos iniciais
        }

        public void incrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        public bool existeMovimentosPossiveis() // testa se há movimentos possíveis para a posição selecionada
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j]) // existe um movimento possível para a posição selecionada
                    {
                        return true;
                    }
                }
            }
            return false; // não há movimentos para a posição selecionada
        }

        public bool podeMoverPara(Posicao pos) // verifica se a peça pode ser movida para a posição selecioada
        {
            return movimentosPossiveis()[pos.linha, pos.coluna]; // testa os movimentos possíveis para a posição informada
        }

        public abstract bool[,] movimentosPossiveis(); // metodo sobre movimentos possíveis por peça, é especializada e individual por peça
    }
}
