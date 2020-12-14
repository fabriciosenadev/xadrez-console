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
    }
}
