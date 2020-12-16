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

        public PartidaDeXadrez()
        {
            this.tab = new Tabuleiro(8, 8); // informa o tamanho do tabuleiro
            this.turno = 1;                 // contagem inicial do turno
            this.jogadorAtual = Cor.Branca; // cor das peças que inicia o jogo
            terminada = false;              // indica o termino da partida
            colocarPecas();                 // coloca as peças declaradas no tabuleiro
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);               // remove a peça da posição de origem informada
            p.incrementarQteMovimentos();                   // incrementa a quantidade de movimentos
            Peca pecaCapturada = tab.retirarPeca(destino);  // informa a posição destino para que caso haja uma peça adversária, seja removida do tabuleiro
            tab.colocarPeca(p, destino);                    // coloca a peça no destino informado
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

        private void colocarPecas()
        {
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}
