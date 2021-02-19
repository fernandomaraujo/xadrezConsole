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

                    // Pegar a posição, e transformar ela pra posição de matriz
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().paraPosicao();

                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().paraPosicao();

                    // E executa movimento
                    partida.executaMovimento(origem, destino);
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
