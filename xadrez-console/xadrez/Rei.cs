using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "R";
        }

        //verifica se uma casa está livre ou com uma peça adversária
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor; // verifica se a casa destino está vazia ou se tem uma peça adversaria
        }

        public override bool[,] movimentosPossiveis()
        { // movimentos possíveis para o Rei
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            // marcar as posições que o rei pode mover
            // verifica se o rei pode mover para cima
            pos.definirValores(posicao.linha - 1, posicao.coluna); // posicao herdado de Peca
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
            }

            // verifica se o rei pode mover para nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1); // posicao herdado de Peca
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
            }

            // verifica se o rei pode mover para direita
            pos.definirValores(posicao.linha, posicao.coluna + 1); // posicao herdado de Peca
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
            }

            // verifica se o rei pode mover para sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1); // posicao herdado de Peca
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
            }

            // verifica se o rei pode mover para baixo
            pos.definirValores(posicao.linha + 1, posicao.coluna); // posicao herdado de Peca
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
            }

            // verifica se o rei pode mover para sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1); // posicao herdado de Peca
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
            }

            // verifica se o rei pode mover para esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1); // posicao herdado de Peca
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
            }

            // verifica se o rei pode mover para noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1); // posicao herdado de Peca
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true; // informa para a matriz que as posições são válidas para movimento da peça
            }

            return mat;
        }
    }
}
