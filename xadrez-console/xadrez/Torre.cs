using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "T";
        }

        //verifica se uma casa está livre ou com uma peça adversária
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor; // verifica se a casa destino está vazia ou se tem uma peça adversaria
        }

        public override bool[,] movimentosPossiveis()
        { // movvimentos possíveis para a Torre

            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);
            // marcar as posições que o rei pode mover

            // verifica se a torre pode mover para cima
            pos.definirValores(posicao.linha - 1, posicao.coluna); // posicao herdado de Peca
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) // valida se existe uma peça adversária para parar o movimento
                {
                    break;
                }
                pos.linha = pos.linha - 1;// vai para a próxima posição acima
            }

            // verifica se a torre pode mover para baixo
            pos.definirValores(posicao.linha + 1, posicao.coluna); // posicao herdado de Peca
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) // valida se existe uma peça adversária para parar o movimento
                {
                    break;
                }
                pos.linha = pos.linha + 1;// vai para a próxima posição abaixo
            }

            // verifica se a torre pode mover para direita
            pos.definirValores(posicao.linha, posicao.coluna + 1); // posicao herdado de Peca
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) // valida se existe uma peça adversária para parar o movimento
                {
                    break;
                }
                pos.coluna = pos.coluna + 1;// vai para a próxima posição a direita
            }

            // verifica se a torre pode mover para esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1); // posicao herdado de Peca
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) // valida se existe uma peça adversária para parar o movimento
                {
                    break;
                }
                pos.coluna = pos.coluna - 1;// vai para a próxima posição a esquerda
            }

            return mat;
        }
    }
}
