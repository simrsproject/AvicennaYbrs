using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class McuPost
    {
        public int kdMCU { get; set; }
        public string noKunjungan { get; set; }
        public string kdProvider { get; set; }
        public string tglPelayanan { get; set; }
        public int tekananDarahSistole { get; set; }
        public int tekananDarahDiastole { get; set; }
        public string radiologiFoto { get; set; }
        public int darahRutinHemo { get; set; }
        public int darahRutinLeu { get; set; }
        public int darahRutinErit { get; set; }
        public int darahRutinLaju { get; set; }
        public int darahRutinHema { get; set; }
        public int darahRutinTrom { get; set; }
        public int lemakDarahHDL { get; set; }
        public int lemakDarahLDL { get; set; }
        public int lemakDarahChol { get; set; }
        public int lemakDarahTrigli { get; set; }
        public int gulaDarahSewaktu { get; set; }
        public int gulaDarahPuasa { get; set; }
        public int gulaDarahPostPrandial { get; set; }
        public int fungsiHatiSGOT { get; set; }
        public int fungsiHatiSGPT { get; set; }
        public int fungsiHatiGamma { get; set; }
        public int fungsiHatiProtKual { get; set; }
        public int fungsiHatiAlbumin { get; set; }
        public int fungsiGinjalCrea { get; set; }
        public int fungsiGinjalUreum { get; set; }
        public int fungsiGinjalAsam { get; set; }
        public int fungsiJantungABI { get; set; }
        public string fungsiJantungEKG { get; set; }
        public string fungsiJantungEcho { get; set; }
        public int? urinRutin { get; set; }
        public string funduskopi { get; set; }
        public string pemeriksaanLain { get; set; }
        public string keterangan { get; set; }
    }
}
