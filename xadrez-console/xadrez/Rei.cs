using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida; // permite que o rei veja toda a partida
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

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

        private bool testeTorreParaRoque(Posicao pos) // teste elegibilidade para roque
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && qteMovimentos == 0;
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

            // jogada especial: roque
            if (qteMovimentos == 0 && !partida.xeque)
            {
                // jogada especial: roque pequeno
                Posicao posicaoTorreProxima = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posicaoTorreProxima))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null) // verifica se está livre entre o rei e a torre
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true; // seta o rei para mover 3 coluna para a direita
                    }
                }

                // jogada especial: roque grande
                Posicao posicaoTorreDistante = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posicaoTorreDistante))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) // verifica se está livre entre o rei e a torre
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true; // seta o rei para mover 3 coluna para a direita
                    }
                }
            }

            return mat;
        }
    }
}
