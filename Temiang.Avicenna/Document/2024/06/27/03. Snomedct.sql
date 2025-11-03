SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Snomedct](
	[SRSnomedct] [varchar](20) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Display] [varchar](200) NOT NULL,
	[DisplayNative] [varchar](200) NULL,
	[Note] [varchar](2000) NULL,
	[IsActive] [bit] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_SnomedctItem] PRIMARY KEY CLUSTERED 
(
	[SRSnomedct] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'102830001',N'Renal angle tenderness',N'Nyeri pada sudut kostovertebra','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'111516008',N'Blurring of visual image',N'Mata kabur','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'116289008',N'Abdominal bloating',N'Kembung','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'11833005',N'Dry cough',N'Batuk kering','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'1209208002',N'Pale Face',N'Wajah pucat','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'12184005',N'Visual field defect',N'Gangguan lapang pandang','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'13791008',N'Asthenia',N'Lemah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'139394000',N'Nocturia',N'Nokturia','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'14302001',N'Amenorrhea',N'Haid terhenti','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'14686007',N'Hemianesthesia',N'Hemianesthesi','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'14760008',N'Constipation ',N'Konstipasi ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'161882006',N'Stiff neck',N'Kaku leher ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'161891005',N'Backache',N'Nyeri punggung','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'16205300',N'Suprapubic pain',N'Nyeri suprapubik','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'162128006',N'Poor stream of urine',N'Pancaran urin mengecil','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'16932000',N'Nausea and vomiting',N'Mual dan muntah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'17173007',N'Excessive thirst',N'Polidipsi','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'18165001',N'Jaundice',N'Kuning','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'18425006',N'Passage of rice water stools (finding) |',N'Feses seperti cucian beras','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'193462001',N'Insomnia',N'Insomnia','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'193982009',N'Epiphora',N'Epifora','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'20022000',N'Hemiparesis',N'Hemiparesis','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'20262006',N'Ataxia',N'Ataksia','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'214264003',N'Lethargy',N'Letargi ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'21522001',N'Abdominal pain',N'Nyeri perut','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'22253000',N'Pain',N'Nyeri','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'224960004',N'Tired',N'Lelah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'225549006',N'Yellow skin',N'Kulit kuning','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'24184005',N'Elevated blood pressure',N'Kenaikan tekanan darah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'24199005',N'Feeling agitated',N'Gelisah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'246975001',N'Scleral icterus',N'Mata kuning','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'247508004',N'White nails',N'Kuku putih','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'248132003',N'Craving for food or drink',N'Ngidam','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'248268002',N'Tires quickly',N'Mudah lelah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'249279003',N'Must strain to pass urine',N'Mengejan saat BAK','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'249366005',N'Bleeding from nose',N'Mimisan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'249473004',N'Altered appetite',N'Perubahan nafsu makan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'24982008',N'Diplopia',N'Diplopia (Penglihatan ganda)','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'2506400',N'Headache ',N'Sakit kepala ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'25064002',N'Headache',N'Nyeri kepala','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'257552002',N'Inflammation',N'Peradangan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'26079004',N'Tremor',N'Tremor','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'267023007',N'Excessive eating - polyphagia',N'Polifagia','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'267036007',N'Dyspnea',N'Sesak nafas ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'267055007',N'Black feces',N'BAB hitam','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'267095009',N'Speech problem',N'Gangguan bicara','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'267096005',N'Frontal headache',N'Nyeri kepala bagian depan ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'26710200',N'Sore throat',N'Nyeri tenggorokan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'267102003',N'Sore throat',N'Nyeri tenggorokan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'271594007',N'Syncope',N'Pingsan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'271687003',N'Swelling of scrotum',N'Skrotum bengkak','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'271782001',N'Drowsy',N'Mudah mengantuk','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'271795006',N'Malaise and fatigue',N'Malaise dan kelelahan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'271807003',N'Skin rash',N'Ruam kulit','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'271823003',N'Tachypnea',N'Takipnea ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'271864008',N'Mucus stool',N'BAB lendir','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'271939006',N'Vaginal discharge',N'Keluar cairan dari vagina','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'274640006',N'Fever with chills',N'Demam menggigil ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'275258002',N'Breast changes',N'Perubahan bentuk payudara','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'278528006',N'Facial swelling',N'Pembengkakan wajah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'279039007',N'Low back pain',N'Nyeri pinggang','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'28442001',N'Polyuria',N'Poliuria','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'28743005',N'Productive cough',N'Batuk berdahak','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'288509005',N'Burning of skin',N'Rasa terbakar','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'288980000',N'Difficulty sucking',N'Kesulitan saat menyusu','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'289195008',N'Slurred speech',N'Cadel','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'29857009',N'Chest pain',N'Nyeri dada','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'3,87E+13',N'Abnormal urination',N'Gangguan BAK','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'300848003',N'Mass of body structure',N'Benjolan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'301345002',N'Difficulty sleeping',N'Sulit tidur','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'30233002',N'Swallowing painful',N'Nyeri telan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'304213008',N'Low-grade fever',N'Demam ringan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'309737007',N'Abdominal pain in pregnancy',N'Nyeri perut pada kehamilan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'34095006',N'Dehydration',N'Dehidrasi','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'3424008',N'Tachycardia',N'Takikardia','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'34436003',N'Hematuria',N'BAK berdarah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'367391008',N'Malaise',N'Malaise','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'36955009',N'Augesia ',N'Augesia (kehilangan pengecapan) ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'372283008',N'Large breast',N'Pembesaran Payudara','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'386661006',N'Fever',N'Demam','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'386813002',N'Abnormal breathing',N'Gangguan napas','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'39575007',N'Urine looks dark',N'Urin gelap','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'398032003',N'Loose stool',N'BAB lembek','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'398212009',N'Watery stool',N'BAB cair','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'399122003',N'Swallowing Problem',N'Sulit menelan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'399153001',N'Vertigo',N'Vertigo','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'399907009',N'Animal bite wound',N'Luka gigitan hewan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'404640003',N'Dizziness',N'Pusing','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'405729008',N'Bloody stool',N'BAB darah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'40739000',N'Dysphagia',N'Nyeri telan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'40917007',N'Clouded consciousness',N'Kebingungan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'409668002',N'Photophobia',N'Fotophobia','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'415690000',N'Sweating',N'Berkeringat','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'418290006',N'Itching',N'Gatal','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'419045004',N'Loss of consciousness',N'Penurunan kesadaran ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'420103007',N'Watery eye',N'Mata berair','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'422400008',N'Vomiting',N'Muntah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'422587007',N'Nausea',N'Mual','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'424131007',N'Easy bruising',N'Mudah memar','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'42984000',N'Night sweats',N'Keringat malam','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'43724002',N'Chill',N'Menggigil','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'44169009',N'Anosmia',N'Anosmia (kehilangan penciuman) ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'442672001',N'Swelling',N'Pembengkakan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'443371007',N'Decreased level of consciousness',N'Penurunan kesadaran','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'444237009',N'Risk of exposure to Leptospira (situation)',N'Risiko paparan Leptospirosis ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'444397009',N'Exposure to Leptospira (event)',N'Paparan terhadap leptospirosis','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'44548000',N'Hyperactivity',N'Hiperaktif','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'44695005',N'Paralysis',N'Paralisis','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'45007003',N'Low blood pressure',N'Tekanan darah rendah ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'45352006',N'Spasm',N'Spasme','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'473434003',N'Pain in chin',N'Nyeri dagu','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'47821001',N'Postpartum hemorrhage',N'Perdarahan setelah melahirkan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'48694002',N'Anxiety',N'Cemas','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'49650001',N'Dysuria',N'Nyeri saat BAK','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'49727002',N'Cough',N'Batuk','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'50091001',N'Petechiae',N'Petekie','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'50582007',N'Hemiplegia',N'Hemiplegi','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'50960005',N'Hemorrhage',N'Perdarahan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'51885006',N'Morning sickness',N'Mual muntah pagi hari','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'52613005',N'Excessive sweating',N'Keringat berlebihan ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'53057004',N'Hand pain',N'Nyeri tangan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'55300003',N'Cramp',N'Kram ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'57676002',N'Arthralgia',N'Nyeri sendi','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'60862001',N'Tinnitus',N'Telinga mendenging','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'62315008',N'Diarrhea',N'Diare','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'62476001',N'Disorientated',N'Disorientasi','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'62507009',N'Pins and needles',N'Kesemutan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'64379006',N'Decrease in appetite',N'Penurunan nafsu makan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'64531003',N'Nasal discharge',N'Pilek','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'66857006',N'Hemoptysis',N'Batuk darah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'67882000',N'Pruritus of vulva',N'Pruritus vulvae','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'68235000',N'Nasal congestion',N'Hidung tersumbat','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'68962001',N'Muscle pain',N'Nyeri otot','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'698193000',N'Coating of mucous membrane of tongue',N'Lidah kotor ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'7011001',N'Hallucinations',N'Halusinasi','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'703630003',N'Red eye',N'Mata merah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'70396004',N'Acholic stool',N'Tinja seperti dempul','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'718403007',N'Decreased urine output',N'BAK sedikit','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'76067001',N'Sneezing',N'Bersin','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'77674003',N'Hemianopia',N'Hemianopsia','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'786837007',N'Tingling',N'Kesemutan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'78691002',N'Staggering gait',N'Jalan sempoyongan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'789507005',N'Delayed healing of wound',N'Luka sulit sembuh','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'7973008',N'Abnormal vision',N'Gangguan pengelihatan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'79890006',N'Loss of appetite',N'Hilang nafsu makan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'79922009',N'Epigastric pain',N'Nyeri epigastrium','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'80313002',N'Palpitations',N'Palpitasi','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'81680005',N'Neck pain',N'Nyeri leher','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'83547004',N'Cold sweat',N'Keringat dingin','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'840546002',N'Exposure to severe acute respiratory syndrome coronavirus 2 (event)',N'Paparan COVID-19 ','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'84229001',N'Fatigue',N'Kelelahan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'86276007',N'Bleeding gums',N'Gusi berdarah','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'87486003',N'Aphasia',N'Afasia','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'8765009',N'Hematemesis',N'Hematemesis','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'88746009',N'Black urine',N'Warna urine seperti teh','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'89362005',N'Weight loss',N'Penurunan berat badan','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'91175000',N'Seizure',N'Kejang','',1,NULL,NULL);
INSERT INTO Snomedct VALUES(N'ChiefComplaint',N'95629002',N'Excessive crying of newborn',N'Tangis berlebihan pada bayi baru lahir ','',1,NULL,NULL);
