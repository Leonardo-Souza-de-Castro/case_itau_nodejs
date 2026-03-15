using System;
using System.Collections.Generic;

namespace Banco.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public double? Saldo { get; set; }
}
