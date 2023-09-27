﻿using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    [Table("TB_NOTICIA")]//cria nome do banco
    public class Noticia : Notifica //Aqui esta herdando a classe de notificações, aonde tem os parametros passados 
    {
        [Column("NTC_TITULO")]
        [MaxLength(255)]
        public string? Titulo { get; set; }

        [Column("NTC_INFORMACAO")]
        [MaxLength (255)]
        public string? Informacao { get; set; }
        
        [Column("NTC_ATIVO")]
        public bool Ativo { get; set; }

        [Column("NTC_DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Column("NTC_DATA_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }


        [ForeignKey("AplicationUser")]
        [Column(Order = 1)]
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
