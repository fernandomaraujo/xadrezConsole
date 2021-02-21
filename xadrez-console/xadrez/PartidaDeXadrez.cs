using System.Collections.Generic;
using tabuleiro;

// Mecânicas do xadrez
namespace xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        
        // Conjunto de todas as peças da partida
        private HashSet<Peca> pecas;

        // Conjunto de todas as peças capturadas
        private HashSet<Peca> capturadas;

        public bool xeque { get; private set; }

        // Se peça está passível de jogada En Passant
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;

            // No xadrez, as peças brancas iniciam o jogo
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;

            // Importante instânciar antes de as peças serem colocadas
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {

            Peca p = tab.retirarPeca(origem);

            // Se moveu, incrementa
            p.incrementarQuantMovimentos();

            // Se tiver uma peça na posição de destino, retira ela
            Peca pecaCapturada = tab.retirarPeca(destino);

            tab.colocarPeca(p, destino);

            // Se houve uma peça capturada (tendo peça no destino)
            if(pecaCapturada != null)
            {
                // Agora adiciona peça no conjunto de peças capturadas
                capturadas.Add(pecaCapturada);

            }

            // Jogada Especial: Roque Pequeno. Se a jogada foi executada
            if(p is Rei && destino.coluna == origem.coluna + 2)
            {
                // Origem da Torre
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);

                // Destino da Torre
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);

                // Retira a Torre e incrementa movimentos
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQuantMovimentos();

                // Coloca Torre no destino
                tab.colocarPeca(T, destinoT);
            }


            // Jogada Especial: Roque Grande. Se a jogada foi executada
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                // Origem da Torre
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);

                // Destino da Torre
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);

                // Retira a Torre e incrementa movimentos
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQuantMovimentos();

                // Coloca Torre no destino
                tab.colocarPeca(T, destinoT);
            }

            // Jogada Especial: En Passant. Se a jogada foi executada
            if(p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == null)
                {

                    Posicao posP;
                    
                    // Se a cor dela for branca
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }

                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }


            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {

            // Retirando peça
            Peca p = tab.retirarPeca(destino);

            // Decrementa movimentos
            p.decrementarQuantMovimentos();

            // Se existiu uma peça que foi capturada
            if(pecaCapturada != null)
            {

                // Colocar ela de volta na posição de destino
                tab.colocarPeca(pecaCapturada, destino);

                // Retirando ela do conjunto de peças capturadas
                capturadas.Remove(pecaCapturada);
            }

            // Colocando peça na posição de volta
            tab.colocarPeca(p, origem);

            // Jogada Especial: Roque Pequeno. Se a jogada foi executada
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);

                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);

                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQuantMovimentos();

                tab.colocarPeca(T, origemT);
            }

            // Jogada Especial: Roque Grande. Se a jogada foi executada
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                // Origem da Torre
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);

                // Destino da Torre
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);

                // Retira a Torre e incrementa movimentos
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQuantMovimentos();

                // Coloca Torre no destino
                tab.colocarPeca(T, destinoT);
            }

            // Jogada Especial: En Passant. Se a jogada foi executada
            if(p is Peao)
            {

                if(origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {

                    Peca peao = tab.retirarPeca(destino);

                    Posicao posP;

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

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            // Se executou um movimento e capturou uma peça
            Peca pecaCapturada =  executaMovimento(origem, destino);

            // O jogador não se pode colocar em xeque. Então caso acontece, desfazer a jogada
            if(estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);

                throw new TabuleiroException("Você não pode se colocar em xeque. Aperte Enter para tentar novamente!");
            }

            // Se o jogador adversário que estiver em xeque, jogada é permitida
            if(estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;

            } else 
            {
                xeque = false;
            }

            // Se após jogada, o adversário estiver em xequemate
            if(testeXequemate(adversaria(jogadorAtual)))
            {
                // Partida termina!
                terminada = true;

            } else
            {
                // Incrementa turno pra mudar jogador
                turno++;
                mudaJogador();
            }

            // Pegando a peça que foi movida
            Peca p = tab.peca(destino);

            // Jogada Especial: En Passant
            // Se o Peão andou duas linhas a mais ou a menos pela primeira vez
            if(p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2 ))
            {

                // Peça está vulnerável
                vulneravelEnPassant = p;

            } else
            {
                // Não tem peça vulnerável
                vulneravelEnPassant = null;
            }

        }

        // Testa se posição de escolha do jogador é válida
        public void validarPosicaoDeOrigem(Posicao pos)
        {

            // Se não tem peça
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida. Aperte Enter para tentar novamente!");
            }

            // Se o jogador atual for reponsável diferente pela cor atual das peças da jogada
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua. Aperte Enter para tentar novamente!");
            }

            // Se a peça está bloqueada de movimentos (não existem movimentos para ela)
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida. Aperte Enter para tentar novamente!");
            }
        }

        // Testa se posição de escolha do jogador é válida
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {

            // Se peça de origem não pode mover para posição de destino
            if(!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida. Aperte Enter para tentar novamente!");
            }
        }


        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            } else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        // Identificando cores das peças que foram capturadas
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {

            // Conjunto auxiliar
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca p in capturadas)
            {
                // Adicionionado no conjunto se a peça for da mesma cor da peça que veio como parâmetro
                if(p.cor == cor)
                {
                    aux.Add(p);
                }
            }

            // Retorna conjunto auxiliar
            return aux;
        }

        // Identificando as peças em jogo, de determinada cor
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            // Conjunto auxiliar
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca p in pecas)
            {
                // Adicionionado no conjunto se a peça for da mesma cor da peça que veio como parâmetro
                if (p.cor == cor)
                {
                    aux.Add(p);
                }
            }

            // E retirando todas as peças da mesma cor, que foram capturadas
            aux.ExceptWith(pecasCapturadas(cor));

            // Retorna conjunto auxiliar
            return aux;
        }

        // Identificando a peça Rei de uma cor
        private Peca rei(Cor cor)
        {

            foreach (Peca p in pecasEmJogo(cor))
            {
                // Peca é uma superclasse, Rei é uma subclasse de Peca
                if(p is Rei)
                {
                    return p;
                }
            }

            // Se não encontrou nenhum Rei (não deve acontecer, toda partida tem reis)
            return null;
        }

        // Identificando todos os movimentos possíveis de todas as peças
        public bool estaEmXeque(Cor cor)
        {
            // Pegando o rei da cor atual
            Peca R = rei(cor);

            // Não deve acontecer, mas caso não haja rei
            if(R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro.");
            }


            // Para cada matriz de movimentos possíveis, se algum movimento bate com uma posição do rei

            // Nas peças advesárias
            foreach (Peca p in pecasEmJogo(adversaria(cor)))
            {

                // Para cada peça, pega seus movimentos possíveis
                bool[,] matriz = p.movimentosPossiveis();

                /* 
                    Se na matriz de movimentos possíveis da peça adversária,
                    na posição onde estiver o rei for true, essa peça adversária
                    pode atacar o rei (está em xeque)
                */

                if(matriz[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }

            // Se não encontrou posição que ameace o rei (não está em xeque)
            return false;

        }

        // Identificando se rei de tal cor está em xeque-mate
        public bool testeXequemate(Cor cor)
        {
            // Se não estiver em xeque, não está em xequemate
            if(!estaEmXeque(cor))
            {
                return false;
            }

            // Procurando alguma peça que movendo, tira jogador do xeque
            foreach (Peca p in pecasEmJogo(cor))
            {

                // Matriz de movimentos possíveis dessa peça
                bool[,] matriz = p.movimentosPossiveis();
                int i, j;

                for(i=0; i<tab.linhas; i++)
                {
                    for(j=0; j<tab.colunas; j++)
                    {

                        // Se movimento for possível
                        if(matriz[i,j])
                        {
                            Posicao origem = p.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);

                            // Se ainda está em xeque, desfaz
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);

                            // Se não está mais em xeque, movimento anterior tirou do xeque
                            if(!testeXeque)
                            {
                                // Não está em xequemate
                                return false;
                            }
                        }

                    }
                }

            }

            // Se nenhuma peça retira do xeque, então é xequemate!
            return true;

        }

        // Identificando oponente
        private Cor adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;

            } else
            {
                return Cor.Branca;
            }

        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {

            // Dado uma coluna e linha, inserir a peça no tabuleiro da partida
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).paraPosicao());

            // Agora adiciona peça no conjunto de peças da partida
            pecas.Add(peca);
        }

        
        private void colocarPecas()
        {

            // Peças brancas
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


            // Peças pretas
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
