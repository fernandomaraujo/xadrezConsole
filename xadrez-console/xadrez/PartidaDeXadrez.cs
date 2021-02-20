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

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;

            // No xadrez, as peças brancas iniciam o jogo
            jogadorAtual = Cor.Branca;
            terminada = false;

            // Importante instânciar antes de as peças serem colocadas
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

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

            // Se houve uma peça capturada (tendo peça no destino)
            if(pecaCapturada != null)
            {
                // Agora adiciona peça no conjunto de peças capturadas
                capturadas.Add(pecaCapturada);

            }
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
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));


            // Peças pretas
            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

        }
    }
}
