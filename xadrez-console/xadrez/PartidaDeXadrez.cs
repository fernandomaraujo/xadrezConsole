using System;
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

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;

            // No xadrez, as peças brancas iniciam o jogo
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {

            Peca p = tab.retirarPeca(origem);

            // Se moveu, incrementa
            p.incrementarQuantMovimentos();

            // Se tiver uma peça na posição de destino, retira ela
            Peca pecaCapturada = tab.retirarPeca(destino);

            tab.colocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);

            // Incrementa turno pra mudar jogador
            turno++;
            mudaJogador();

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
            if(!tab.peca(origem).podeMoverPara(destino))
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

        private void colocarPecas()
        {

            // Peças brancas
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).paraPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).paraPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).paraPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).paraPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).paraPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).paraPosicao());


            // Peças pretas
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).paraPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).paraPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).paraPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).paraPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).paraPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).paraPosicao());
        }
    }
}
