namespace tabuleiro
{
    abstract class Peca
    {

        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int quantMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            this.quantMovimentos = 0;
        }

        /*
            Matriz de booleanos

            true = Onde movimento for possível;
            false = Onde movimento não for possível;
         */
        public abstract bool[,] movimentosPossiveis();

        public void incrementarQuantMovimentos()
        {
            quantMovimentos++;
        }
    }
}
