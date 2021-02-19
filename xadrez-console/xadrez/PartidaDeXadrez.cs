using System;
using tabuleiro;

// Mecânicas do xadrez
namespace xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
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
