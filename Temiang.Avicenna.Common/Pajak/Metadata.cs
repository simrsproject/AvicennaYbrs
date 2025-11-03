using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Common.Pajak
{
    public class Metadata
    {
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class resValidateFakturPm
        {

            private byte kdJenisTransaksiField;

            private byte fgPenggantiField;

            private string nomorFakturField;

            private string tanggalFakturField;

            private ulong npwpPenjualField;

            private string namaPenjualField;

            private string alamatPenjualField;

            private ulong npwpLawanTransaksiField;

            private string namaLawanTransaksiField;

            private string alamatLawanTransaksiField;

            private uint jumlahDppField;

            private uint jumlahPpnField;

            private byte jumlahPpnBmField;

            private string statusApprovalField;

            private string statusFakturField;

            private resValidateFakturPmDetailTransaksi[] detailTransaksiField;

            /// <remarks/>
            public byte kdJenisTransaksi
            {
                get
                {
                    return this.kdJenisTransaksiField;
                }
                set
                {
                    this.kdJenisTransaksiField = value;
                }
            }

            /// <remarks/>
            public byte fgPengganti
            {
                get
                {
                    return this.fgPenggantiField;
                }
                set
                {
                    this.fgPenggantiField = value;
                }
            }

            /// <remarks/>
            public string nomorFaktur
            {
                get
                {
                    return this.nomorFakturField;
                }
                set
                {
                    this.nomorFakturField = value;
                }
            }

            /// <remarks/>
            public string tanggalFaktur
            {
                get
                {
                    return this.tanggalFakturField;
                }
                set
                {
                    this.tanggalFakturField = value;
                }
            }

            /// <remarks/>
            public ulong npwpPenjual
            {
                get
                {
                    return this.npwpPenjualField;
                }
                set
                {
                    this.npwpPenjualField = value;
                }
            }

            /// <remarks/>
            public string namaPenjual
            {
                get
                {
                    return this.namaPenjualField;
                }
                set
                {
                    this.namaPenjualField = value;
                }
            }

            /// <remarks/>
            public string alamatPenjual
            {
                get
                {
                    return this.alamatPenjualField;
                }
                set
                {
                    this.alamatPenjualField = value;
                }
            }

            /// <remarks/>
            public ulong npwpLawanTransaksi
            {
                get
                {
                    return this.npwpLawanTransaksiField;
                }
                set
                {
                    this.npwpLawanTransaksiField = value;
                }
            }

            /// <remarks/>
            public string namaLawanTransaksi
            {
                get
                {
                    return this.namaLawanTransaksiField;
                }
                set
                {
                    this.namaLawanTransaksiField = value;
                }
            }

            /// <remarks/>
            public string alamatLawanTransaksi
            {
                get
                {
                    return this.alamatLawanTransaksiField;
                }
                set
                {
                    this.alamatLawanTransaksiField = value;
                }
            }

            /// <remarks/>
            public uint jumlahDpp
            {
                get
                {
                    return this.jumlahDppField;
                }
                set
                {
                    this.jumlahDppField = value;
                }
            }

            /// <remarks/>
            public uint jumlahPpn
            {
                get
                {
                    return this.jumlahPpnField;
                }
                set
                {
                    this.jumlahPpnField = value;
                }
            }

            /// <remarks/>
            public byte jumlahPpnBm
            {
                get
                {
                    return this.jumlahPpnBmField;
                }
                set
                {
                    this.jumlahPpnBmField = value;
                }
            }

            /// <remarks/>
            public string statusApproval
            {
                get
                {
                    return this.statusApprovalField;
                }
                set
                {
                    this.statusApprovalField = value;
                }
            }

            /// <remarks/>
            public string statusFaktur
            {
                get
                {
                    return this.statusFakturField;
                }
                set
                {
                    this.statusFakturField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("detailTransaksi")]
            public resValidateFakturPmDetailTransaksi[] detailTransaksi
            {
                get
                {
                    return this.detailTransaksiField;
                }
                set
                {
                    this.detailTransaksiField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class resValidateFakturPmDetailTransaksi
        {

            private string namaField;

            private uint hargaSatuanField;

            private byte jumlahBarangField;

            private uint hargaTotalField;

            private uint diskonField;

            private uint dppField;

            private decimal ppnField;

            private byte tarifPpnbmField;

            private byte ppnbmField;

            /// <remarks/>
            public string nama
            {
                get
                {
                    return this.namaField;
                }
                set
                {
                    this.namaField = value;
                }
            }

            /// <remarks/>
            public uint hargaSatuan
            {
                get
                {
                    return this.hargaSatuanField;
                }
                set
                {
                    this.hargaSatuanField = value;
                }
            }

            /// <remarks/>
            public byte jumlahBarang
            {
                get
                {
                    return this.jumlahBarangField;
                }
                set
                {
                    this.jumlahBarangField = value;
                }
            }

            /// <remarks/>
            public uint hargaTotal
            {
                get
                {
                    return this.hargaTotalField;
                }
                set
                {
                    this.hargaTotalField = value;
                }
            }

            /// <remarks/>
            public uint diskon
            {
                get
                {
                    return this.diskonField;
                }
                set
                {
                    this.diskonField = value;
                }
            }

            /// <remarks/>
            public uint dpp
            {
                get
                {
                    return this.dppField;
                }
                set
                {
                    this.dppField = value;
                }
            }

            /// <remarks/>
            public decimal ppn
            {
                get
                {
                    return this.ppnField;
                }
                set
                {
                    this.ppnField = value;
                }
            }

            /// <remarks/>
            public byte tarifPpnbm
            {
                get
                {
                    return this.tarifPpnbmField;
                }
                set
                {
                    this.tarifPpnbmField = value;
                }
            }

            /// <remarks/>
            public byte ppnbm
            {
                get
                {
                    return this.ppnbmField;
                }
                set
                {
                    this.ppnbmField = value;
                }
            }
        }
    }
}
