
URL		:~/GenericRpt/GetJson.asmx/Get
Params	:
	- accessKey:
	- jSonQueryAndParam:

kerangka jSonQueryAndParam:
{	"query":{	
		"ds1":{"sQuery":"select aa, @bb, cc from xxx where field = '@param1'","returnType":0},
		"ds2":{"sQuery":"select gg, hh, ii from yyy where field = '@param2'","returnType":0},
	},
	"param":{	"@param1":"reg1234",
		"@param2":"reg2222",
		"@bb":{"sQuery":"select xx, yy, zz from zzz where field = '@param3'","returnType":1},
		"@param3":"reg4444"
	},
	"serviceurl":"GenericRpt/GetJson/Get"
}
kerangka return
{	"ds1":[
		{"aa":"value aa1", "bb":{"xx":"value xx", "yy":"value yy", "zz":"value zz"}, "cc":"value cc1"},
		{"aa":"value aa2", "bb":{"xx":"value xx", "yy":"value yy", "zz":"value zz"}, "cc":"value cc2"},
	],
	"ds2":[{"gg":"value gg", "hh":"value hh", "ii":"value ii"}],
}

contoh jSonQueryAndParam:
{	"query":{	
		"ds1":{
			"sQuery":"SELECT r.RegistrationNo, r.ServiceUnitID, r.ParamedicID, r.SRRegistrationType @Add1 FROM Registration AS r WHERE r.RegistrationNo = @RegistrationNo",
			"returnType":1},
		"ds2":{
			"sQuery":"SELECT tp.PaymentNo, tp.TotalPaymentAmount FROM TransPayment AS tp WHERE tp.RegistrationNo = @RegistrationNo",
			"returnType":1}
	},
	"param":{	
		"@RegistrationNo":"REG/IP/191001-0002",
		"@Add1":{"sQuery":"SELECT asri.ItemID, asri.ItemName, CASE asri.ItemID WHEN '@SRRegistrationType' THEN 1 ELSE 0 END Selected FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = @RegType","returnType":0},
		"@RegType":"RegistrationType"
	},
	"serviceurl":"GenericRpt/GetJson/Get"
}

returnType:
	- 0 --> return array --> contoh: kerangka return: ds1, ds2
	- 1 --> return non array --> contoh: kerangka return: bb