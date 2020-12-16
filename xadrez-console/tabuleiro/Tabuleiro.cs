using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        public Peca[,] pecas; // matriz privativa

        public Tabuleiro(int linhas, int colunas)
        {
            // this indica a prop quando o parametro tem a mesma nomenclatura
            this.linhas = linhas;
            this.colunas = colunas;

            pecas = new Peca[linhas, colunas]; //inicia com os argumentos informados
        }

        // metodo que retorna uma peca, usando linha e coluna como parametros
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        //sobrecarga de peça recebendo uma posição especificada - melhoria
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }

        public void colocarPeca(Peca p, Posicao pos)
        {
            // verifica se foi feita tentativa de colocar duas peças na mesma posição
            if (existePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição!");
            }

            pecas[pos.linha, pos.coluna] = p; // adiciona peça na posição informada
            p.posicao = pos; // informa a posição da peça
        }

        // retira uma peça do tabuleiro considerando a posicação informada
        public Peca retirarPeca(Posicao pos)
        {
            if (peca(pos) == null) // não há peça nesta posição
            {
                return null;
            }
            Peca aux = peca(pos); // armazena a peça na posição informada
            aux.posicao = null; // passa a posição da peça para null, informando que foi retirada do tabuleiro
            pecas[pos.linha, pos.coluna] = null; // informa ao conjunto de peças que naquela posição a peça foi removida
            return aux; // retorna a peça que foi armazenada
        }

        // metodo para testar se existe uma peça na posição informada
        public bool existePeca(Posicao pos)
        {
            // verifica se a posição informada é valida
            validarPosicao(pos);
            // retorna verdadeiro caso a peça esteja na posição
            return peca(pos) != null;
        }

        // metodo para testar se uma posição é valida
        public bool posicaoValida(Posicao pos)
        {
            // valida das posições iniciais até as posições declaradas no contrutor
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }

        // validação de posição para tratamento com excessão
        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
            {
                //lança excessão
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
