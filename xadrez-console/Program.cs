using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                PartidaDeXadrez partida = new PartidaDeXadrez();
                
                // Enquanto a partida não for terminada
                while(!partida.terminada)
                {

                    // Limpar o terminal
                    Console.Clear();

                    // Imprime tabuleiro
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Turno: " + partida.turno);
                    Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

                    // Pegar a posição, e transformar ela pra posição de matriz
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().paraPosicao();

                    // Guardando na matriz os movimentos possíveis da peça
                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                    // Limpar o terminal
                    Console.Clear();

                    // Imprime tabuleiro, com os movimentos possíveis para peça
                    Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.WriteLine();

                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().paraPosicao();

                    // E realiza a jogada
                    partida.realizaJogada(origem, destino);
                }

                Tela.imprimirTabuleiro(partida.tab);

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
