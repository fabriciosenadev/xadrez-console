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
        public bool xeque { get; private set; } // controle de xeque da partida
        public Peca vulneravelEnPassant { get; private set; } // prop para a jogada especial En Passant

        public PartidaDeXadrez()
        {
            this.tab = new Tabuleiro(8, 8); // informa o tamanho do tabuleiro
            this.turno = 1;                 // contagem inicial do turno
            this.jogadorAtual = Cor.Branca; // cor das peças que inicia o jogo
            terminada = false;              // indica o termino da partida
            xeque = false;                  // indica que a posição atual coloca o rei adversário em xeque
            vulneravelEnPassant = null;     // indica vulnerabilidade para jogada especial En Passant

            //conjuntos de peças
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            colocarPecas();                 // coloca as peças declaradas no tabuleiro
        }

        public Peca executaMovimento(Posicao origem, Posicao destino) // executa movimento da Peca e retorna as peças capturadas
        {
            Peca p = tab.retirarPeca(origem);               // remove a peça da posição de origem informada
            p.incrementarQteMovimentos();                   // incrementa a quantidade de movimentos
            Peca pecaCapturada = tab.retirarPeca(destino);  // informa a posição destino para que caso haja uma peça adversária, seja removida do tabuleiro
            tab.colocarPeca(p, destino);                    // coloca a peça no destino informado

            // peças capturadas
            if (pecaCapturada != null) // verifica se existe uma peça que foi capturada
            {
                capturadas.Add(pecaCapturada); // adiciona ao conjunto das peças capturadas
            }

            // jogada especial: roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemTorre);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoTorre);
            }

            // jogada especial: roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemTorre);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoTorre);
            }

            // jogada especial: en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null) // verifica se mexeu na diagonal e não tem peça
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna); // captura a peça na linha de baixo
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna); // captura a peça na linha de cima
                    }
                    // faz a captura manual da peça 
                    pecaCapturada = tab.retirarPeca(posP); 
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) // reverte os movimentos realizados
        {
            Peca p = tab.retirarPeca(destino); // remove a peça da posição de destino informada
            p.decrementarQteMovimentos(); // decrementa a quantidade de movimentos
            if (pecaCapturada != null) // verifica se houve uma peça captura de uma peça
            {
                tab.colocarPeca(pecaCapturada, destino); // coloca a peça capturada de volta no destino
                capturadas.Remove(pecaCapturada); // remove a peça do conjunto de peças capturadas
            }
            tab.colocarPeca(p, origem); // coloca a peca na posição de origem

            // jogada especial: roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemTorre);
            }

            // jogada especial: roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemTorre);
            }

            // jogada especial: en passant
            if(p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant) // quando ocorre captura por en passant
                {
                    Peca peao = tab.retirarPeca(destino); // remove o peão recolocado, pois a posição fica incorreta
                    Posicao posP;
                    // adiciona na posição correta de acordo com a cor da peça
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) // faz a jogada acontecer de fato
        {
            Peca pecaCapturadas = executaMovimento(origem, destino);  // executa o movimento e retorna peça capturada

            if (estaEmXeque(jogadorAtual)) // verifica se o jogador que moveu está em xeque
            {
                // será necessário desfazer a jogada
                desfazMovimento(origem, destino, pecaCapturadas);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = tab.peca(destino); // pega a peça movida

            // jogada especia: promocao
            if(p is Peao)
            {
                // verifica se o peão dechou na ultima casa adversária
                if((p.cor == Cor.Branca && destino.linha == 0) || (p.cor == Cor.Preta && destino.linha == 7)) 
                {
                    // remove o peão do tabuleiro e das peças em jogo
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);

                    // adiciona uma dama no lugar o peão retirado
                    Peca dama = new Dama(tab, p.cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }


            if (estaEmXeque(adversaria(jogadorAtual))) // verifica se o adversário está em xeque
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual))) // verifica se o adversário está em xequemate
            {
                terminada = true; // informa que partida terminou terminou
            }
            else
            {
                turno++;        // passa o turno
                mudaJogador();  // muda o jogador atual
            }

            

            // jogada especial: en passant
            // se o peão moveu pela primeira vez
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)) // verifica as posições vulneráveis do peao
            { // informa que ele está vulnerável a toma um en passant
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
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
            if (!tab.peca(origem).movimentoPossivel(destino)) // valida se a peça de origem não pode ser movida para a posição de destino
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
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor) // verifica se a cor da peça capturada é a mesma cor passada por parametro
                {
                    aux.Add(x);
                }
            }
            return aux; // retorna o conjunto filtrado pela cor
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) // separa as peças em jogo pela cor informada
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor) // verifica se a cor da peça em jogo é a mesma cor passada por parametro
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor)); //remove do conjunto as peças capturadas da cor informada
            return aux; // retorna o conjunto filtrado pela cor
        }

        private Cor adversaria(Cor cor) // definição de jogador adversário
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor) // procura o rei da cor informada
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei) // quando o rei é encontrado
                {
                    return x;
                }
            }
            return null; // rei não foi encontrado
        }

        public bool estaEmXeque(Cor cor) // Testa se o rei da cor informada está em xeque
        {
            Peca R = rei(cor);

            if (R == null) // veriifca se tem rei no tabuleiro
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + "");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) // para cada peça em jogo da cor adversária
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) // verifica se o rei está em xeque
                {
                    return true;
                }
            }
            return false; // quando rei não está em xeque
        }

        public bool testeXequemate(Cor cor) // testa se o rei da cor informada está em xequemate
        {
            if (!estaEmXeque(cor)) // verifica se o rei está em xeque
            {
                return false;
            }

            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j]) // verifica posições possíveis
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino); // tenta executar o movime nto para esta posição
                            bool testeXeque = estaEmXeque(cor); // testa se ainda está em xeque
                            desfazMovimento(origem, destino, pecaCapturada); // desfaz o movimento

                            if (!testeXeque) // verifica se não está mais em xeque pelo movimento testtado
                            {
                                return false; // não está em xeque
                            }
                        }
                    }
                }
            }
            return true; // está em xeque
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) // metodo auxiliar para colocação de peças no tabuleiro
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); // coloca as peças informadas na posição declarada
            pecas.Add(peca); // adiciona a peça informada ao conjunto de peças disponíveis na partida
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}
