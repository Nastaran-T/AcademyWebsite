using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Academy.DataLayer.Entities.Wallet
{
    public class WalletType
    {
        public WalletType()
        {

        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int  TypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public String TypeTitle { get; set; }

        #region Relations
        public List<Wallet> Wallets { get; set; }
        #endregion
    }
}

