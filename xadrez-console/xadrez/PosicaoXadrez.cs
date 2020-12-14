using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {// posição para exibição no tabuleiro de xadrez
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        // converte a posição do tabuleiro para uma posição de matriz
        public Posicao toPosicao()
        {
            // numero da linha, código da coluna
            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString()
        {
            return String.Format("{0}{1}", coluna, linha);
        }
    }
}
