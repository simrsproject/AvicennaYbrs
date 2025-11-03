using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class EyePe : BaseJsonField
    {
        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        private EyeTest _rightEye;
        public EyeTest RightEye
        {
            get { return _rightEye ?? (_rightEye = new EyeTest()); }
            set { _rightEye = value; }
        }
        private EyeTest _leftEye;
        public EyeTest LeftEye
        {
            get { return _leftEye ?? (_leftEye = new EyeTest()); }
            set { _leftEye = value; }
        }

        private AbNormalAndNotes _LVisus;
        public AbNormalAndNotes LVisus
        {
            get { return _LVisus ?? (_LVisus = new AbNormalAndNotes()); }
            set { _LVisus = value; }
        }

        private AbNormalAndNotes _RVisus;
        public AbNormalAndNotes RVisus
        {
            get { return _RVisus ?? (_RVisus = new AbNormalAndNotes()); }
            set { _RVisus = value; }
        }

        private AbNormalAndNotes _LRefractio;
        public AbNormalAndNotes LRefractio
        {
            get { return _LRefractio ?? (_LRefractio = new AbNormalAndNotes()); }
            set { _LRefractio = value; }
        }

        private AbNormalAndNotes _RRefractio;
        public AbNormalAndNotes RRefractio
        {
            get { return _RRefractio ?? (_RRefractio = new AbNormalAndNotes()); }
            set { _RRefractio = value; }
        }

        private AbNormalAndNotes _LTension;
        public AbNormalAndNotes LTension
        {
            get { return _LTension ?? (_LTension = new AbNormalAndNotes()); }
            set { _LTension = value; }
        }

        private AbNormalAndNotes _RTension;
        public AbNormalAndNotes RTension
        {
            get { return _RTension ?? (_RTension = new AbNormalAndNotes()); }
            set { _RTension = value; }
        }

        private AbNormalAndNotes _LCorrection;
        public AbNormalAndNotes LCorrection
        {
            get { return _LCorrection ?? (_LCorrection = new AbNormalAndNotes()); }
            set { _LCorrection = value; }
        }

        private AbNormalAndNotes _RCorrection;
        public AbNormalAndNotes RCorrection
        {
            get { return _RCorrection ?? (_RCorrection = new AbNormalAndNotes()); }
            set { _RCorrection = value; }
        }

        private AbNormalAndNotes _LGlasses;
        public AbNormalAndNotes LGlasses
        {
            get { return _LGlasses ?? (_LGlasses = new AbNormalAndNotes()); }
            set { _LGlasses = value; }
        }

        private AbNormalAndNotes _RGlasses;
        public AbNormalAndNotes RGlasses
        {
            get { return _RGlasses ?? (_RGlasses = new AbNormalAndNotes()); }
            set { _RGlasses = value; }
        }


        private AbNormalAndNotes _LOcular;
        public AbNormalAndNotes LOcular
        {
            get { return _LOcular ?? (_LOcular = new AbNormalAndNotes()); }
            set { _LOcular = value; }
        }

        private AbNormalAndNotes _ROcular;
        public AbNormalAndNotes ROcular
        {
            get { return _ROcular ?? (_ROcular = new AbNormalAndNotes()); }
            set { _ROcular = value; }
        }

        private AbNormalAndNotes _LAnterior;
        public AbNormalAndNotes LAnterior
        {
            get { return _LAnterior ?? (_LAnterior = new AbNormalAndNotes()); }
            set { _LAnterior = value; }
        }


        private AbNormalAndNotes _RAnterior;
        public AbNormalAndNotes RAnterior
        {
            get { return _RAnterior ?? (_RAnterior = new AbNormalAndNotes()); }
            set { _RAnterior = value; }
        }

        private AbNormalAndNotes _LPosterior;
        public AbNormalAndNotes LPosterior
        {
            get { return _LPosterior ?? (_LPosterior = new AbNormalAndNotes()); }
            set { _LPosterior = value; }
        }

        private AbNormalAndNotes _RPosterior;
        public AbNormalAndNotes RPosterior
        {
            get { return _RPosterior ?? (_RPosterior = new AbNormalAndNotes()); }
            set { _RPosterior = value; }
        }

        private AbNormalAndNotes _LEyeBallPosition;
        public AbNormalAndNotes LEyeBallPosition
        {
            get { return _LEyeBallPosition ?? (_LEyeBallPosition = new AbNormalAndNotes()); }
            set { _LEyeBallPosition = value; }
        }

        private AbNormalAndNotes _REyeBallPosition;
        public AbNormalAndNotes REyeBallPosition
        {
            get { return _REyeBallPosition ?? (_REyeBallPosition = new AbNormalAndNotes()); }
            set { _REyeBallPosition = value; }
        }

        private AbNormalAndNotes _LEyeBallMovement;
        public AbNormalAndNotes LEyeBallMovement
        {
            get { return _LEyeBallMovement ?? (_LEyeBallMovement = new AbNormalAndNotes()); }
            set { _LEyeBallMovement = value; }
        }

        private AbNormalAndNotes _REyeBallMovement;
        public AbNormalAndNotes REyeBallMovement
        {
            get { return _REyeBallMovement ?? (_REyeBallMovement = new AbNormalAndNotes()); }
            set { _REyeBallMovement = value; }
        }


        private AbNormalAndNotes _LConfrontation;
        public AbNormalAndNotes LConfrontation
        {
            get { return _LConfrontation ?? (_LConfrontation = new AbNormalAndNotes()); }
            set { _LConfrontation = value; }
        }

        private AbNormalAndNotes _RConfrontation;
        public AbNormalAndNotes RConfrontation
        {
            get { return _RConfrontation ?? (_RConfrontation = new AbNormalAndNotes()); }
            set { _RConfrontation = value; }
        }


        private AbNormalAndNotes _LOrbitalBone;
        public AbNormalAndNotes LOrbitalBone
        {
            get { return _LOrbitalBone ?? (_LOrbitalBone = new AbNormalAndNotes()); }
            set { _LOrbitalBone = value; }
        }

        private AbNormalAndNotes _ROrbitalBone;
        public AbNormalAndNotes ROrbitalBone
        {
            get { return _ROrbitalBone ?? (_ROrbitalBone = new AbNormalAndNotes()); }
            set { _ROrbitalBone = value; }
        }

        private AbNormalAndNotes _LPalpebra;
        public AbNormalAndNotes LPalpebra
        {
            get { return _LPalpebra ?? (_LPalpebra = new AbNormalAndNotes()); }
            set { _LPalpebra = value; }
        }


        private AbNormalAndNotes _RPalpebra;
        public AbNormalAndNotes RPalpebra
        {
            get { return _RPalpebra ?? (_RPalpebra = new AbNormalAndNotes()); }
            set { _RPalpebra = value; }
        }

        private AbNormalAndNotes _LConjungtivaTars;
        public AbNormalAndNotes LConjungtivaTars
        {
            get { return _LConjungtivaTars ?? (_LConjungtivaTars = new AbNormalAndNotes()); }
            set { _LConjungtivaTars = value; }
        }

        private AbNormalAndNotes _RConjungtivaTars;
        public AbNormalAndNotes RConjungtivaTars
        {
            get { return _RConjungtivaTars ?? (_RConjungtivaTars = new AbNormalAndNotes()); }
            set { _RConjungtivaTars = value; }
        }

        private AbNormalAndNotes _LConjungtivaBulbi;
        public AbNormalAndNotes LConjungtivaBulbi
        {
            get { return _LConjungtivaBulbi ?? (_LConjungtivaBulbi = new AbNormalAndNotes()); }
            set { _LConjungtivaBulbi = value; }
        }

        private AbNormalAndNotes _RConjungtivaBulbi;
        public AbNormalAndNotes RConjungtivaBulbi
        {
            get { return _RConjungtivaBulbi ?? (_RConjungtivaBulbi = new AbNormalAndNotes()); }
            set { _RConjungtivaBulbi = value; }
        }

        private AbNormalAndNotes _LSclera;
        public AbNormalAndNotes LSclera
        {
            get { return _LSclera ?? (_LSclera = new AbNormalAndNotes()); }
            set { _LSclera = value; }
        }

        private AbNormalAndNotes _RSclera;
        public AbNormalAndNotes RSclera
        {
            get { return _RSclera ?? (_RSclera = new AbNormalAndNotes()); }
            set { _RSclera = value; }
        }

        private AbNormalAndNotes _LLimbCornea;
        public AbNormalAndNotes LLimbCornea
        {
            get { return _LLimbCornea ?? (_LLimbCornea = new AbNormalAndNotes()); }
            set { _LLimbCornea = value; }
        }

        private AbNormalAndNotes _RLimbCornea;
        public AbNormalAndNotes RLimbCornea
        {
            get { return _RLimbCornea ?? (_RLimbCornea = new AbNormalAndNotes()); }
            set { _RLimbCornea = value; }
        }

        private AbNormalAndNotes _LCornea;
        public AbNormalAndNotes LCornea
        {
            get { return _LCornea ?? (_LCornea = new AbNormalAndNotes()); }
            set { _LCornea = value; }
        }

        private AbNormalAndNotes _RCornea;
        public AbNormalAndNotes RCornea
        {
            get { return _RCornea ?? (_RCornea = new AbNormalAndNotes()); }
            set { _RCornea = value; }
        }

        private AbNormalAndNotes _LCameraOculiAnterior;
        public AbNormalAndNotes LCameraOculiAnterior
        {
            get { return _LCameraOculiAnterior ?? (_LCameraOculiAnterior = new AbNormalAndNotes()); }
            set { _LCameraOculiAnterior = value; }
        }

        private AbNormalAndNotes _RCameraOculiAnterior;
        public AbNormalAndNotes RCameraOculiAnterior
        {
            get { return _RCameraOculiAnterior ?? (_RCameraOculiAnterior = new AbNormalAndNotes()); }
            set { _RCameraOculiAnterior = value; }
        }

        private AbNormalAndNotes _LIris;
        public AbNormalAndNotes LIris
        {
            get { return _LIris ?? (_LIris = new AbNormalAndNotes()); }
            set { _LIris = value; }
        }

        private AbNormalAndNotes _RIris;
        public AbNormalAndNotes RIris
        {
            get { return _RIris ?? (_RIris = new AbNormalAndNotes()); }
            set { _RIris = value; }
        }

        private AbNormalAndNotes _LPupil;
        public AbNormalAndNotes LPupil
        {
            get { return _LPupil ?? (_LPupil = new AbNormalAndNotes()); }
            set { _LPupil = value; }
        }

        private AbNormalAndNotes _RPupil;
        public AbNormalAndNotes RPupil
        {
            get { return _RPupil ?? (_RPupil = new AbNormalAndNotes()); }
            set { _RPupil = value; }
        }

        private AbNormalAndNotes _LLens;
        public AbNormalAndNotes LLens
        {
            get { return _LLens ?? (_LLens = new AbNormalAndNotes()); }
            set { _LLens = value; }
        }

        private AbNormalAndNotes _RLens;
        public AbNormalAndNotes RLens
        {
            get { return _RLens ?? (_RLens = new AbNormalAndNotes()); }
            set { _RLens = value; }
        }


        private AbNormalAndNotes _LFundus;
        public AbNormalAndNotes LFundus
        {
            get { return _LFundus ?? (_LFundus = new AbNormalAndNotes()); }
            set { _LFundus = value; }
        }

        private AbNormalAndNotes _RFundus;
        public AbNormalAndNotes RFundus
        {
            get { return _RFundus ?? (_RFundus = new AbNormalAndNotes()); }
            set { _RFundus = value; }
        }

        private AbNormalAndNotes _LCorpusVitreum;
        public AbNormalAndNotes LCorpusVitreum
        {
            get { return _LCorpusVitreum ?? (_LCorpusVitreum = new AbNormalAndNotes()); }
            set { _LCorpusVitreum = value; }
        }

        private AbNormalAndNotes _RCorpusVitreum;
        public AbNormalAndNotes RCorpusVitreum
        {
            get { return _RCorpusVitreum ?? (_RCorpusVitreum = new AbNormalAndNotes()); }
            set { _RCorpusVitreum = value; }
        }


        private AbNormalAndNotes _LOther;
        public AbNormalAndNotes LOther
        {
            get { return _LOther ?? (_LOther = new AbNormalAndNotes()); }
            set { _LOther = value; }
        }

        private AbNormalAndNotes _ROther;
        public AbNormalAndNotes ROther
        {
            get { return _ROther ?? (_ROther = new AbNormalAndNotes()); }
            set { _ROther = value; }
        }

        private AbNormalAndNotes2 _ishihara2;
        public AbNormalAndNotes2 Ishihara2
        {
            get { return _ishihara2 ?? (_ishihara2 = new AbNormalAndNotes2()); }
            set { _ishihara2 = value; }
        }

        private AbNormalAndNotes2 _ishihara;
        public AbNormalAndNotes2 Ishihara
        {
            get { return _ishihara ?? (_ishihara = new AbNormalAndNotes2()); }
            set { _ishihara = value; }
        }
        public string Notes { get; set; }
        public string NutritionSkrinning { get; set; }

    }
    public class EyeTest
    {
        public string Visus { get; set; }
        public string Refractio { get; set; }
        public string Tension { get; set; }
        public string Correction { get; set; }
        public string Glasses { get; set; }
        public string Ocular { get; set; }
        public string Anterior { get; set; }
        public string Posterior { get; set; }

        public string EyeBallPosition { get; set; }
        public string EyeBallMovement { get; set; }
        public string Confrontation { get; set; }
        public string OrbitalBone { get; set; }
        public string Palpebra { get; set; }
        public string ConjungtivaTars { get; set; }
        public string ConjungtivaBulbi { get; set; }
        public string Sclera { get; set; }
        public string LimbCornea { get; set; }
        public string Cornea { get; set; }
        public string CameraOculiAnterior { get; set; }
        public string Iris { get; set; }
        public string Pupil { get; set; }
        public string Lens { get; set; }
        public string Fundus { get; set; }
        public string CorpusVitreum { get; set; }
        public string Other { get; set; }

    }
}
