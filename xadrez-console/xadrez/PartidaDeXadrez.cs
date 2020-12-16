using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
using xadrez;

namespace xadrez
{
    class PartidaDeXadrez
    {   // esta classe contem as regras da partida de xadrez

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;        // conjunto de peças da partida
        private HashSet<Peca> capturadas;   // conjunto de peças capturadas

        public PartidaDeXadrez()
        {
            this.tab = new Tabuleiro(8, 8); // informa o tamanho do tabuleiro
            this.turno = 1;                 // contagem inicial do turno
            this.jogadorAtual = Cor.Branca; // cor das peças que inicia o jogo
            terminada = false;              // indica o termino da partida

            //conjuntos de peças
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            colocarPecas();                 // coloca as peças declaradas no tabuleiro
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);               // remove a peça da posição de origem informada
            p.incrementarQteMovimentos();                   // incrementa a quantidade de movimentos
            Peca pecaCapturada = tab.retirarPeca(destino);  // informa a posição destino para que caso haja uma peça adversária, seja removida do tabuleiro
            tab.colocarPeca(p, destino);                    // coloca a peça no destino informado
            
            // peças capturadas
            if(pecaCapturada != null) // verifica se existe uma peça que foi capturada
            {
                capturadas.Add(pecaCapturada); // adiciona ao conjunto das peças capturadas
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) // faz a jogada acontecer de fato
        {
            executaMovimento(origem, destino);  // executa o movimento
            turno++;                            // passa o turno
            mudaJogador();                      // muda o jogador atual
        }

        public void validarPosicaoDeOrigem(Posicao pos) // verifica se a origem é válida
        {
            if (tab.peca(pos) == null) // verifica se a posição não possúi peça 
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (jogadorAtual != tab.peca(pos).cor) //verifica se o jogador atual é diferente da peça selecionada na posição indicada
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!tab.peca(pos).existeMovimentosPossiveis()) // verifica se a peça selecionada na posição indicada está bloqueada para ser movida
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) // verifica com base na origem, se a posição de destino é valida
        {
            if (!tab.peca(origem).podeMoverPara(destino)) // valida se a peça de origem não pode ser movida para a posição de destino
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) // separa as peças capturadas pela cor informada
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor) // verifica se a cor da peça capturada é a mesma cor passada por parametro
                {
                    aux.Add(x);
                }
            }
            return aux; // retorna o conjunto filtrado pela cor
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) // separa as peças em jogo pela cor informada
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in pecas)
            {
                if(x.cor == cor) // verifica se a cor da peça em jogo é a mesma cor passada por parametro
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor)); //remove do conjunto as peças capturadas da cor informada
            return aux; // retorna o conjunto filtrado pela cor
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) // metodo auxiliar para colocação de peças no tabuleiro
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); // coloca as peças informadas na posição declarada
            pecas.Add(peca); // adiciona a peça informada ao conjunto de peças disponíveis na partida
        }

        private void colocarPecas()
        {

            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
        }
    }
}
