namespace Application
{
    public static class Const
    {
        public readonly static string QueueProduto = "PRODUTO/";
        public readonly static string QueueCliente = "CLIENTE/";
        public readonly static string QueueProdutosDelete = "PRODUTOS_DELETE/";
        public readonly static string QueueClienteDelete = "CLIENTES_DELETE/";
        public readonly static string QueueProdutoRemover = "PRODUTOS_REMOVER/";
        public readonly static string ConnectionMqtt = "127.0.0.1";
        public readonly static int ConnectionMqttPort = 1883;
        public static readonly Guid ClientId = Guid.NewGuid();
    }
}
