using Application.Services;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System.Globalization;
using System.Text.Json;

namespace Server.Config
{
    public class Config : IConfig
    {
        private readonly IClienteService clienteService;
        private readonly IProdutoService produtoService;
        public Config(IClienteService clienteService, IProdutoService produtoService)
        {
            this.clienteService = clienteService;
            this.produtoService = produtoService;
        }

        public async Task<string> TratarMensagem(string mensagem)
        {

            if (mensagem.Contains("inserirCliente"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));
                var cid = values[0];
                var nome = RemoverChars(values[1]);

                var cliente = new Cliente
                {
                    ClientId = cid,
                    Name = nome

                };
                var result = await clienteService.InserirCLiente(cliente);
                if (result == 0)
                    return("Cliente já existe");
                else
                   return("Cliente cadastrado com sucesso");

            }

            if (mensagem.Contains("modificarCliente"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));
                var cid = RemoverChars(values[0]);
                var nome = RemoverChars(values[1]);

                var cliente = new Cliente
                {
                    ClientId = cid,
                    Name = nome

                };
                var result = await clienteService.AtualizarCliente(cliente);
                if (result == 0)
                    return ("Cliente não existe");
                else
                    return ("Cliente atualizado com sucesso");
            }

            if (mensagem.Contains("recuperarCliente"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));
                var cid = RemoverChars(values[0]);
         
                var result = await clienteService.BuscarCliente(cid);

                if (result == null)
                    return ("Cliente não existe");
                else
                    return (JsonSerializer.Serialize(result));
            }

            if (mensagem.Contains("apagarCliente"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));
                var cid = RemoverChars(values[0]);

                var result = await clienteService.ApagarCliente(cid);
                if (result == 0)
                    return ("Cliente não existe");
                else
                    return ("Cliente apagado com sucesso");
            }

            if (mensagem.Contains("inserirProduto"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));
                var Pid = values[0];
                var nome = RemoverChars(values[1]);
                decimal preco = decimal.Parse(RemoverChars(values[2]), CultureInfo.InvariantCulture);
                int quantidade = 0;
                int.TryParse(RemoverChars(values[3]), out quantidade);

                var produto = new Produto
                {
                    ProdutctId = Pid,
                    Name = nome,
                    Price = preco,
                    Quantity = quantidade

                };

                var result = await produtoService.InserirProduto(produto);

                if (result == 0)
                    return ("Produto já existe");
                else
                    return ("Produto cadastrado com sucesso");
            }


            if (mensagem.Contains("modificarProduto"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));
                var Pid = values[0];
                var nome = RemoverChars(values[1]);
                decimal preco = decimal.Parse(RemoverChars(values[2]), CultureInfo.InvariantCulture);
                int quantidade = 0;
                int.TryParse(RemoverChars(values[3]), out quantidade);

                var produto = new Produto
                {
                    ProdutctId = Pid,
                    Name = nome,
                    Price = preco,
                    Quantity = quantidade

                };

                var result = await produtoService.AtualizarProduto(produto);

                if (result == 0)
                    return ("Produto não existe");
                else
                    return ("Produto atualizado com sucesso");
            }
            if (mensagem.Equals("recuperarProdutos()"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));

                var result = await produtoService.BuscarProdutos();


                if (result == null)
                    return ("Produto não existe");
                else
                    return (JsonSerializer.Serialize(result));

            }

            if (mensagem.Contains("recuperarProduto"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));
                var pid = RemoverChars(values[0]);

                var result = await produtoService.BuscarProduto(pid);


                if (result == null)
                    return ("Produto não existe");
                else
                    return (JsonSerializer.Serialize(result));
            }

            if (mensagem.Contains("apagarProduto"))
            {
                var itens = mensagem.Split("(");
                var values = itens[1].Split((","));
                var pid = RemoverChars(values[0]);

                var result = await produtoService.ApagarProduto(pid);
                if (result == 0)
                    return ("Produto não existe");
                else
                    return ("Produto apagado com sucesso");
            }
            else
                return "Operação não cadastrada";
        }


        private string RemoverChars(string str)
        {
            var charsToRemove = new string[] { "(", ")", "\n","\r"};
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty).Trim().ToUpper();
            }
            return str;
        }
    }
}
