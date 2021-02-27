using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesForce.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "PedidoSequencia");

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CodigoIbge = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Uf = table.Column<string>(type: "varchar(100)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CondicoesPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicoesPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormasPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Tipo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Tef = table.Column<bool>(nullable: false),
                    Credito = table.Column<bool>(nullable: false),
                    PermitirTroco = table.Column<bool>(nullable: false),
                    ConfiguracaoFiscal = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Sigla = table.Column<string>(type: "varchar(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    CnpjCpfDi = table.Column<string>(type: "varchar(100)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    Numero = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    Cep = table.Column<string>(type: "varchar(100)", nullable: true),
                    CidadeId = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    TipoPessoa = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(100)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "varchar(100)", nullable: true),
                    TipoInscricaoEstadual = table.Column<int>(nullable: false),
                    ConsumidorFinal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Cidades_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    Fantasia = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    CnpjCpfDi = table.Column<string>(type: "varchar(100)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    Numero = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 60, nullable: false),
                    Cep = table.Column<string>(type: "varchar(100)", nullable: true),
                    CidadeId = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    TipoPessoa = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(100)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "varchar(100)", nullable: true),
                    TipoInscricaoEstadual = table.Column<int>(nullable: false),
                    Padrao = table.Column<bool>(nullable: false),
                    Crt = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Cidades_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Estoque = table.Column<double>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    UnidadeId = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    PermiteFracionar = table.Column<bool>(nullable: false),
                    CodigoInterno = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutosServicos_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Codigo = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR PedidoSequencia"),
                    Status = table.Column<int>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    CondicaoPagamentoId = table.Column<Guid>(nullable: false),
                    FormaPagamentoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_CondicoesPagamento_CondicaoPagamentoId",
                        column: x => x.CondicaoPagamentoId,
                        principalTable: "CondicoesPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_FormasPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormasPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedidosItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PedidoId = table.Column<Guid>(nullable: false),
                    Item = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<Guid>(nullable: false),
                    Quantidade = table.Column<double>(nullable: false),
                    ValorUnitario = table.Column<double>(nullable: false),
                    ValorDesconto = table.Column<double>(nullable: false),
                    ValorAcrescimo = table.Column<double>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosItens_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosItens_ProdutosServicos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "ProdutosServicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CidadeId",
                table: "Clientes",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_CidadeId",
                table: "Empresas",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CondicaoPagamentoId",
                table: "Pedidos",
                column: "CondicaoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_FormaPagamentoId",
                table: "Pedidos",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosItens_PedidoId",
                table: "PedidosItens",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosItens_ProdutoId",
                table: "PedidosItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosServicos_UnidadeId",
                table: "ProdutosServicos",
                column: "UnidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "PedidosItens");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "ProdutosServicos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "CondicoesPagamento");

            migrationBuilder.DropTable(
                name: "FormasPagamento");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropSequence(
                name: "PedidoSequencia");
        }
    }
}
