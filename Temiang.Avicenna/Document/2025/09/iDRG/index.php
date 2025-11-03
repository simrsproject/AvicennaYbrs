<?php
require 'vendor/autoload.php';
require 'db.php';
use Slim\Http\Request;
use Slim\Http\Response;
use Slim\App;

/* EKLAIM */

$app = new Slim\App();

$app->get('/', 'index');

$app->post('/test', 'test');

$app->post('/test_ws', 'test_ws');

$app->post('/new_claim', 'new_claim');
$app->post('/update_patient', 'update_patient');
$app->post('/delete_patient', 'delete_patient');
$app->post('/set_claim_data', 'set_claim_data');
$app->post('/pull_claim', 'pull_claim');
$app->post('/grouper1', 'grouper1');
$app->post('/grouper2', 'grouper2');
$app->post('/claim_final', 'claim_final');
$app->post('/idrg_diagnosa_set', 'idrg_diagnosa_set');
$app->post('/idrg_procedure_set', 'idrg_procedure_set');
$app->post('/grouper_idrg', 'grouper_idrg');
$app->post('/send_claim_individual', 'send_claim_individual');
$app->post('/reedit_claim', 'reedit_claim');
$app->post('/get_claim_data', 'get_claim_data');
$app->post('/set_diagnose_data', 'set_diagnose_data');
$app->post('/set_procedure_data', 'set_procedure_data');
$app->post('/delete_claim', 'delete_claim');
$app->post('/claim_print', 'claim_print');
$app->post('/search_diagnosis', 'search_diagnosis');
$app->post('/search_procedures', 'search_procedures');
$app->post('/generate_claim_number', 'generate_claim_number');
$app->post('/file_upload', 'file_upload');
$app->post('/file_delete', 'file_delete');
$app->post('/file_get', 'file_get');
$app->post('/get_diagnose_inagroupper', 'get_diagnose_inagroupper');
$app->post('/get_procedure_inagroupper', 'get_procedure_inagroupper');
$app->post('/set_sitb_validate', 'set_sitb_validate');
$app->post('/set_sitb_invalidate', 'set_sitb_invalidate');
$app->post('/set_claim_transfusi', 'set_claim_transfusi');

// ROUTING API IDRG
$app->post('/new_claim2', 'new_claim2');
$app->post('/set_claim_data2', 'set_claim_data2');
$app->post('/get_claim_data2', 'get_claim_data2');
$app->post('/idrg_diagnosa_set2', 'idrg_diagnosa_set2');
$app->post('/idrg_diagnosa_get2', 'idrg_diagnosa_get2');
$app->post('/idrg_procedure_set2', 'idrg_procedure_set2');
$app->post('/idrg_procedure_get2', 'idrg_procedure_get2');
$app->post('/grouper1idrg2', 'grouper1idrg2');
$app->post('/idrg_grouper_final2', 'idrg_grouper_final2');
$app->post('/idrg_grouper_reedit2', 'idrg_grouper_reedit2');
$app->post('/idrg_to_inacbg_import2', 'idrg_to_inacbg_import2');
$app->post('/inacbg_diagnosa_get2', 'inacbg_diagnosa_get2');
$app->post('/inacbg_diagnosa_set2', 'inacbg_diagnosa_set2');
$app->post('/inacbg_procedure_set2', 'inacbg_procedure_set2');
$app->post('/inacbg_procedure_get2', 'inacbg_procedure_get2');
$app->post('/grouper1inacbg2', 'grouper1inacbg2');
$app->post('/grouper2inacbg2', 'grouper2inacbg2');
$app->post('/inacbg_grouper_final2', 'inacbg_grouper_final2');
$app->post('/inacbg_grouper_reedit2', 'inacbg_grouper_reedit2');
$app->post('/claim_final2', 'claim_final2');
$app->post('/reedit_claim2', 'reedit_claim2');
$app->post('/send_claim_individual2', 'send_claim_individual2');


/* PACS DCM4CHEE MM2100 */
$app->post('/status_pacs_worklist', 'status_pacs_worklist');
$app->post('/new_pacs_worklist', 'new_pacs_worklist');

/* SIRANAP */
$app->post('/siranap_bed_monitoring_new', 'siranap_bed_monitoring_new');
$app->post('/siranap_bed_monitoring_delete', 'siranap_bed_monitoring_delete');

/* SISRUTE */
$app->post('/sisrute_get_rujukan', 'sisrute_get_rujukan');
$app->post('/sisrute_set_rujukan', 'sisrute_set_rujukan');
$app->post('/sisrute_get_diagnosa', 'sisrute_get_diagnosa');
$app->post('/sisrute_get_alasan', 'sisrute_get_alasan');
$app->post('/sisrute_get_faskes', 'sisrute_get_faskes');
$app->post('/sisrute_batal_rujukan', 'sisrute_batal_rujukan');
$app->post('/sisrute_jawab_rujukan', 'sisrute_jawab_rujukan');

/* DISDUKCAPIL */
$app->post('/dukcapil_get_nik', 'dukcapil_get_nik');

/* VKLAIM v1.1 */
$app->post('/vklaim_decrypt', 'vklaim_decrypt');

$app->run();

// FUNCTION POST API
function new_claim2($request, $response, $args) {
	$json = '{
	"metadata":{
		"method":"new_claim"
	},
	"data":{
		"nomor_kartu":"'.$request->getParsedBody()['nomor_kartu'].'",
		"nomor_sep":"'.$request->getParsedBody()['nomor_sep'].'",
		"nomor_rm":"'.$request->getParsedBody()['nomor_rm'].'",
		"nama_pasien":"'.$request->getParsedBody()['nama_pasien'].'",
		"tgl_lahir":"'.$request->getParsedBody()['tgl_lahir'].'",
		"gender":"'.$request->getParsedBody()['gender'].'"
	}}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;	
}

function set_claim_data2($request, $response, $args) {
	$nomor_sep=$request->getParsedBody()['nomor_sep'];
	$nomor_kartu=$request->getParsedBody()['nomor_kartu'];
	$tgl_masuk=$request->getParsedBody()['tgl_masuk'];
	$tgl_pulang=$request->getParsedBody()['tgl_pulang'];
	$cara_masuk=$request->getParsedBody()['cara_masuk'];
	$jenis_rawat=$request->getParsedBody()['jenis_rawat'];
	$kelas_rawat=$request->getParsedBody()['kelas_rawat'];
	$adl_sub_acute=$request->getParsedBody()['adl_sub_acute'];
	$adl_chronic=$request->getParsedBody()['adl_chronic'];
	$icu_indikator=$request->getParsedBody()['icu_indikator']; 
	$icu_los=$request->getParsedBody()['icu_los']; 
	$ventilator_hour=$request->getParsedBody()['ventilator_hour']; 
	
	$use_ind=$request->getParsedBody()['use_ind']; 	
	$start_dttm=$request->getParsedBody()['start_dttm']; 
	$stop_dttm=$request->getParsedBody()['stop_dttm']; 
			
	$upgrade_class_ind=$request->getParsedBody()['upgrade_class_ind']; 
	$upgrade_class_class=$request->getParsedBody()['upgrade_class_class']; 
	$upgrade_class_los=$request->getParsedBody()['upgrade_class_los']; 	
	$upgrade_class_payor=$request->getParsedBody()['upgrade_class_payor'];
	$add_payment_pct=$request->getParsedBody()['add_payment_pct']; 	
	$birth_weight=$request->getParsedBody()['birth_weight']; 
	$sistole=$request->getParsedBody()['sistole'];
	$diastole=$request->getParsedBody()['diastole'];	
	$discharge_status=$request->getParsedBody()['discharge_status']; 
	
	$prosedur_non_bedah=$request->getParsedBody()['prosedur_non_bedah']; 
	$prosedur_bedah=$request->getParsedBody()['prosedur_bedah']; 
	$konsultasi=$request->getParsedBody()['konsultasi']; 
	$tenaga_ahli=$request->getParsedBody()['tenaga_ahli']; 
	$keperawatan=$request->getParsedBody()['keperawatan']; 
	$penunjang=$request->getParsedBody()['penunjang']; 
	$radiologi=$request->getParsedBody()['radiologi']; 
	$laboratorium=$request->getParsedBody()['laboratorium']; 
	$pelayanan_darah=$request->getParsedBody()['pelayanan_darah']; 
	$rehabilitasi=$request->getParsedBody()['rehabilitasi']; 
	$kamar=$request->getParsedBody()['kamar']; 
	$rawat_intensif=$request->getParsedBody()['rawat_intensif'];
	$obat=$request->getParsedBody()['obat']; 
	$obat_kronis=$request->getParsedBody()['obat_kronis']; 
	$obat_kemoterapi=$request->getParsedBody()['obat_kemoterapi']; 
	$alkes=$request->getParsedBody()['alkes']; 
	$bmhp=$request->getParsedBody()['bmhp']; 
	$sewa_alat=$request->getParsedBody()['sewa_alat']; 
	
	$pemulasaraan_jenazah=$request->getParsedBody()['pemulasaraan_jenazah']; 
	$kantong_jenazah=$request->getParsedBody()['kantong_jenazah']; 
	$peti_jenazah=$request->getParsedBody()['peti_jenazah']; 
	$plastik_erat=$request->getParsedBody()['plastik_erat']; 
	$desinfektan_jenazah=$request->getParsedBody()['desinfektan_jenazah']; 
	$mobil_jenazah=$request->getParsedBody()['mobil_jenazah']; 
	$desinfektan_mobil_jenazah=$request->getParsedBody()['desinfektan_mobil_jenazah']; 
	$covid19_status_cd=$request->getParsedBody()['covid19_status_cd']; 
	$nomor_kartu_t=$request->getParsedBody()['nomor_kartu_t']; 
	$episodes=$request->getParsedBody()['episodes']; 
    $covid19_cc_ind=$request->getParsedBody()['covid19_cc_ind']; 
    $covid19_rs_darurat_ind=$request->getParsedBody()['covid19_rs_darurat_ind']; 
    $covid19_co_insidense_ind=$request->getParsedBody()['covid19_co_insidense_ind']; 	
	
    $lab_asam_laktat=$request->getParsedBody()['lab_asam_laktat']; 	
    $lab_procalcitonin=$request->getParsedBody()['lab_procalcitonin']; 	
    $lab_crp=$request->getParsedBody()['lab_crp']; 	
    $lab_kultur=$request->getParsedBody()['lab_kultur']; 	
    $lab_d_dimer=$request->getParsedBody()['lab_d_dimer']; 	
    $lab_pt=$request->getParsedBody()['lab_pt']; 	
    $lab_aptt=$request->getParsedBody()['lab_aptt']; 	
    $lab_waktu_pendarahan=$request->getParsedBody()['lab_waktu_pendarahan']; 	
    $lab_anti_hiv=$request->getParsedBody()['lab_anti_hiv']; 	
    $lab_analisa_gas=$request->getParsedBody()['lab_analisa_gas']; 	
    $lab_albumin=$request->getParsedBody()['lab_albumin']; 	
    $rad_thorax_ap_pa=$request->getParsedBody()['rad_thorax_ap_pa']; 	

    $terapi_konvalesen=$request->getParsedBody()['terapi_konvalesen']; 	
    $akses_naat=$request->getParsedBody()['akses_naat']; 	
    $isoman_ind=$request->getParsedBody()['isoman_ind']; 	
    $bayi_lahir_status_cd=$request->getParsedBody()['bayi_lahir_status_cd']; 

    $dializer_single_use=$request->getParsedBody()['dializer_single_use']; 
	$kantong_darah=$request->getParsedBody()['kantong_darah']; 
	
	$menit_1_appearance=$request->getParsedBody()['menit_1_appearance']; 
	$menit_1_pulse=$request->getParsedBody()['menit_1_pulse']; 
	$menit_1_grimace=$request->getParsedBody()['menit_1_grimace']; 
	$menit_1_activity=$request->getParsedBody()['menit_1_activity']; 
	$menit_1_respiration=$request->getParsedBody()['menit_1_respiration']; 
	$menit_5_appearance=$request->getParsedBody()['menit_5_appearance']; 
	$menit_5_pulse=$request->getParsedBody()['menit_5_pulse']; 
	$menit_5_grimace=$request->getParsedBody()['menit_5_grimace']; 
	$menit_5_activity=$request->getParsedBody()['menit_5_activity']; 
	$menit_5_respiration=$request->getParsedBody()['menit_5_respiration']; 
	$usia_kehamilan=$request->getParsedBody()['usia_kehamilan']; 
	$gravida=$request->getParsedBody()['gravida']; 
	$partus=$request->getParsedBody()['partus']; 
	$abortus=$request->getParsedBody()['abortus']; 
	$onset_kontraksi=$request->getParsedBody()['onset_kontraksi']; 
	$delivery=$request->getParsedBody()['delivery']; 
	
	$tarif_poli_eks=$request->getParsedBody()['tarif_poli_eks']; 
	$nama_dokter=$request->getParsedBody()['nama_dokter']; 
	$kode_tarif=$request->getParsedBody()['kode_tarif']; 
	$payor_id=$request->getParsedBody()['payor_id']; 
	$payor_cd=$request->getParsedBody()['payor_cd']; 
	$cob_cd=$request->getParsedBody()['cob_cd']; 
	$coder_nik=$request->getParsedBody()['coder_nik']; 	

    $json = '{
	"metadata":{
		"method":"set_claim_data",
		"nomor_sep":"'.$nomor_sep.'"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'",
		"nomor_kartu":"'.$nomor_kartu.'",
		"tgl_masuk":"'.$tgl_masuk.'",
		"tgl_pulang":"'.$tgl_pulang.'",
		"cara_masuk":"'.$cara_masuk.'",
		"jenis_rawat":"'.$jenis_rawat.'",
		"kelas_rawat":"'.$kelas_rawat.'",
		"adl_sub_acute":"'.$adl_sub_acute.'",
		"adl_chronic":"'.$adl_chronic.'",
		"icu_indikator":"'.$icu_indikator.'",
		"icu_los":"'.$icu_los.'",
		"ventilator_hour":"'.$ventilator_hour.'",
		"ventilator": { 
			"use_ind": "'.$use_ind.'", 
			"start_dttm": "'.$start_dttm.'", 
			"stop_dttm": "'.$stop_dttm.'" 
		},
		"upgrade_class_ind":"'.$upgrade_class_ind.'",
		"upgrade_class_class":"'.$upgrade_class_class.'",
		"upgrade_class_los":"'.$upgrade_class_los.'",
		"upgrade_class_payor": "'.$upgrade_class_payor.'",
		"add_payment_pct": "'.$add_payment_pct.'",
		"birth_weight":"'.$birth_weight.'",
		"sistole":"'.$sistole.'", 
		"diastole":"'.$diastole.'",
		"discharge_status":"'.$discharge_status.'",
		"tarif_rs":{
			"prosedur_non_bedah":"'.$prosedur_non_bedah.'", 
			"prosedur_bedah":"'.$prosedur_bedah.'",
			"konsultasi":"'.$konsultasi.'",
			"tenaga_ahli":"'.$tenaga_ahli.'",
			"keperawatan":"'.$keperawatan.'",
			"penunjang":"'.$penunjang.'",
			"radiologi":"'.$radiologi.'",
			"laboratorium":"'.$laboratorium.'",
			"pelayanan_darah":"'.$pelayanan_darah.'",
			"rehabilitasi":"'.$rehabilitasi.'",
			"kamar":"'.$kamar.'",
			"rawat_intensif":"'.$rawat_intensif.'",
			"obat":"'.$obat.'",
			"obat_kronis":"'.$obat_kronis.'",
			"obat_kemoterapi":"'.$obat_kemoterapi.'",
			"alkes":"'.$alkes.'",
			"bmhp":"'.$bmhp.'",
			"sewa_alat":"'.$sewa_alat.'"
		},
		"pemulasaraan_jenazah":"'.$pemulasaraan_jenazah.'", 
		"kantong_jenazah":"'.$kantong_jenazah.'", 
		"peti_jenazah":"'.$peti_jenazah.'", 
		"plastik_erat":"'.$plastik_erat.'", 
		"desinfektan_jenazah":"'.$desinfektan_jenazah.'", 
		"mobil_jenazah":"'.$mobil_jenazah.'", 
		"desinfektan_mobil_jenazah":"'.$desinfektan_mobil_jenazah.'", 
		"covid19_status_cd":"'.$covid19_status_cd.'", 
		"nomor_kartu_t":"'.$nomor_kartu_t.'", 
		"episodes":"'.$episodes.'", 
		"covid19_cc_ind":"'.$covid19_cc_ind.'",
		"covid19_rs_darurat_ind": "'.$covid19_rs_darurat_ind.'",
		"covid19_co_insidense_ind": "'.$covid19_co_insidense_ind.'",
		"covid19_penunjang_pengurang":{
			"lab_asam_laktat" : "'.$lab_asam_laktat.'",
			"lab_procalcitonin" : "'.$lab_procalcitonin.'",
			"lab_crp" : "'.$lab_crp.'",
			"lab_kultur" : "'.$lab_kultur.'",
			"lab_d_dimer" : "'.$lab_d_dimer.'",
			"lab_pt" : "'.$lab_pt.'",
			"lab_aptt" : "'.$lab_aptt.'",
			"lab_waktu_pendarahan" : "'.$lab_waktu_pendarahan.'",
			"lab_anti_hiv" : "'.$lab_anti_hiv.'",
			"lab_analisa_gas" : "'.$lab_analisa_gas.'",
			"lab_albumin" : "'.$lab_albumin.'",
			"rad_thorax_ap_pa" : "'.$rad_thorax_ap_pa.'"
		}, 
		"terapi_konvalesen":"'.$terapi_konvalesen.'",
		"akses_naat": "'.$akses_naat.'",
		"isoman_ind":"'.$isoman_ind.'",
		"bayi_lahir_status_cd":"'.$bayi_lahir_status_cd.'",
		"dializer_single_use":"'.$dializer_single_use.'", 
		"kantong_darah":"'.$kantong_darah.'", 
		"apgar": { 
			"menit_1": { 
				"appearance":"'.$menit_1_appearance.'", 
				"pulse":"'.$menit_1_pulse.'", 
				"grimace":"'.$menit_1_grimace.'", 
				"activity":"'.$menit_1_activity.'", 
				"respiration":"'.$menit_1_respiration.'" 
			}, 
			"menit_5": { 
				"appearance":"'.$menit_5_appearance.'", 
				"pulse":"'.$menit_5_pulse.'", 
				"grimace":"'.$menit_5_grimace.'", 
				"activity":"'.$menit_5_activity.'", 
				"respiration":"'.$menit_5_respiration.'" 
			} 
		}, 
		"persalinan": { 
			"usia_kehamilan":"'.$usia_kehamilan.'", 
			"gravida":"'.$gravida.'", 
			"partus":"'.$partus.'", 
			"abortus":"'.$abortus.'", 
			"onset_kontraksi":"'.$onset_kontraksi.'", 
			"delivery":'.$delivery.'
		},
		"tarif_poli_eks":"'.$tarif_poli_eks.'",
		"nama_dokter":"'.$nama_dokter.'",
		"kode_tarif":"'.$kode_tarif.'",
		"payor_id":"'.$payor_id.'",
		"payor_cd":"'.$payor_cd.'",
		"cob_cd":"'.$cob_cd.'",
		"coder_nik":"'.$coder_nik.'"
		}}';

	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

function idrg_diagnosa_set2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"idrg_diagnosa_set",
		"nomor_sep":"'.$nomor_sep.'"
	},
	"data":{
		"diagnosa":"'.$request->getParsedBody()['diagnosa'].'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function idrg_diagnosa_get2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"idrg_diagnosa_set"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function idrg_procedure_set2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"idrg_procedure_set",
		"nomor_sep":"'.$nomor_sep.'"
	},
	"data":{
		"procedure":"'.$request->getParsedBody()['procedure'].'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function idrg_procedure_get2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"idrg_procedure_set"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function grouper1idrg2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"grouper",
		"stage":"1",
		"grouper":"idrg"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function idrg_grouper_final2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"idrg_grouper_final"	
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function idrg_grouper_reedit2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"idrg_grouper_reedit"	
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function idrg_to_inacbg_import2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"idrg_to_inacbg_import"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function inacbg_diagnosa_set2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"inacbg_diagnosa_set",
		"nomor_sep":"'.$nomor_sep.'"
	},
	"data":{
		"diagnosa":"'.$request->getParsedBody()['diagnosa'].'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function inacbg_diagnosa_get2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"inacbg_diagnosa_get"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function inacbg_procedure_set2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"inacbg_procedure_set",
		"nomor_sep":"'.$nomor_sep.'"
	},
	"data":{
		"procedure":"'.$request->getParsedBody()['procedure'].'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function inacbg_procedure_get2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"inacbg_procedure_get"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function grouper1inacbg2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"grouper",
		"stage":"1",
		"grouper":"inacbg"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function grouper2inacbg2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	$special_cmg = $request->getParsedBody()['special_cmg'];
	
	$json = '{
	"metadata":{
		"method":"grouper",
		"stage":"2",
		"grouper":"inacbg"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'",
		"special_cmg":"'.$special_cmg.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function inacbg_grouper_final2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"inacbg_grouper_final"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function inacbg_grouper_reedit2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"inacbg_grouper_reedit"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function claim_final2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	$coder_nik = $request->getParsedBody()['coder_nik'];
	
	$json = '{  
	"metadata":{  
		"method":"claim_final"
	},
	"data":{  
		"nomor_sep":"'.$nomor_sep.'",
		"coder_nik":"'.$coder_nik.'"
	}}';
   
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);
	
	$result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

function reedit_claim2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{  
	"metadata":{  
		"method":"reedit_claim"
	},
	"data":{  
		"nomor_sep":"'.$nomor_sep.'"
	}}';
   
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);
	
	$result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

function send_claim_individual2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{  
	"metadata":{  
		"method":"send_claim_individual"
	},
	"data":{  
		"nomor_sep":"'.$nomor_sep.'"
	}}';
   
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);
	
	$result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

/* METHOD GET DISINI */
/* ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
function index() {
	Redirect(getUrlApp(), false);
}

function Redirect($url, $permanent = false) {
	if (headers_sent() === false) {
        header('Location: ' . $url, true, ($permanent === true) ? 301 : 302);
    }
    exit();
}

function test($request, $response, $args) {
	$value=$request->getParsedBody()['value'];
	$json = '{
		"metadata":{
			"method":"test"
		},
		"data":{
			"value":'.$value.'
		}}';

    $response->write($json);	
	return $response;	
}

function test_ws($request, $response, $args) {
	$json = '{
		"metadata": {
			"method":"sitb_validate"
		},
		"data": {
			"nomor_sep": "0301R0110223V005102", 
			"nomor_register_sitb": "2023000101400006"
		}}';

	// data yang akan dikirimkan dengan method POST adalah encrypted: 
	$payload = mc_encrypt($json, getKey()); 
	
	// tentukan Content-Type pada http header 	
	$header = array("Content-Type: application/x-www-form-urlencoded"); 
	
	// url server aplikasi E-Klaim
	$url = getUrlWS(); 
	
	// setup curl 
	$ch = curl_init(); 
	curl_setopt($ch, CURLOPT_URL, $url); 
	curl_setopt($ch, CURLOPT_HEADER, 0); 
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1); 
	curl_setopt($ch, CURLOPT_HTTPHEADER,$header); 
	curl_setopt($ch, CURLOPT_POST, 1); 
	curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
	
	// request dengan curl 
	$result = curl_exec($ch); 
	
	// terlebih dahulu hilangkan "----BEGIN ENCRYPTED DATA----\r\n" 
	// dan hilangkan "----END ENCRYPTED DATA----\r\n" dari response 
	$first = strpos($result, "\n") + 1; 
	$last = strrpos($result, "\n") - 1; 
	
	$result = substr($result, $first, strlen($result) - $first - $last);
	$result = mc_decrypt(getKey(), $result);
 
    $response->write($result);	
	return $result;	
}

function new_claim($request, $response, $args) {
	$json = '{
	"metadata":{
		"method":"new_claim"
	},
	"data":{
		"nomor_kartu":"'.$request->getParsedBody()['nomor_kartu'].'",
		"nomor_sep":"'.$request->getParsedBody()['nomor_sep'].'",
		"nomor_rm":"'.$request->getParsedBody()['nomor_rm'].'",
		"nama_pasien":"'.$request->getParsedBody()['nama_pasien'].'",
		"tgl_lahir":"'.$request->getParsedBody()['tgl_lahir'].'",
		"gender":"'.$request->getParsedBody()['gender'].'"
	}}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;	
}

function update_patient($request, $response, $args) {
	$json = '{
	"metadata":{
		"method":"update_patient",
		"nomor_rm":"'.$request->getParsedBody()['nomor_rm'].'"
	},
	"data":{
		"nomor_kartu":"'.$request->getParsedBody()['nomor_kartu'].'",
		"nomor_rm":"'.$request->getParsedBody()['nomor_rm'].'",
		"nama_pasien":"'.$request->getParsedBody()['nama_pasien'].'",
		"tgl_lahir":"'.$request->getParsedBody()['tgl_lahir'].'",
		"gender":"'.$request->getParsedBody()['gender'].'"
	}}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;	
}

function delete_patient($request, $response, $args) {
	$json = '{
		"metadata":{
		"method":"delete_patient"
	},
	"data":{
		"nomor_rm":"'.$request->getParsedBody()['nomor_rm'].'",
		"coder_nik":"'.$request->getParsedBody()['coder_nik'].'"
	}}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;	
}

function set_claim_data($request, $response, $args) {
	$nomor_sep=$request->getParsedBody()['nomor_sep'];
	$nomor_kartu=$request->getParsedBody()['nomor_kartu'];
	$tgl_masuk=$request->getParsedBody()['tgl_masuk'];
	$tgl_pulang=$request->getParsedBody()['tgl_pulang'];
	$cara_masuk=$request->getParsedBody()['cara_masuk'];
	$jenis_rawat=$request->getParsedBody()['jenis_rawat'];
	$kelas_rawat=$request->getParsedBody()['kelas_rawat'];
	$adl_sub_acute=$request->getParsedBody()['adl_sub_acute'];
	$adl_chronic=$request->getParsedBody()['adl_chronic'];
	$icu_indikator=$request->getParsedBody()['icu_indikator']; 
	$icu_los=$request->getParsedBody()['icu_los']; 
	$ventilator_hour=$request->getParsedBody()['ventilator_hour']; 
	
	$use_ind=$request->getParsedBody()['use_ind']; 	
	$start_dttm=$request->getParsedBody()['start_dttm']; 
	$stop_dttm=$request->getParsedBody()['stop_dttm']; 
			
	$upgrade_class_ind=$request->getParsedBody()['upgrade_class_ind']; 
	$upgrade_class_class=$request->getParsedBody()['upgrade_class_class']; 
	$upgrade_class_los=$request->getParsedBody()['upgrade_class_los']; 	
	$upgrade_class_payor=$request->getParsedBody()['upgrade_class_payor'];
	$add_payment_pct=$request->getParsedBody()['add_payment_pct']; 	
	$birth_weight=$request->getParsedBody()['birth_weight']; 
	$sistole=$request->getParsedBody()['sistole'];
	$diastole=$request->getParsedBody()['diastole'];	
	$discharge_status=$request->getParsedBody()['discharge_status']; 
	
	$prosedur_non_bedah=$request->getParsedBody()['prosedur_non_bedah']; 
	$prosedur_bedah=$request->getParsedBody()['prosedur_bedah']; 
	$konsultasi=$request->getParsedBody()['konsultasi']; 
	$tenaga_ahli=$request->getParsedBody()['tenaga_ahli']; 
	$keperawatan=$request->getParsedBody()['keperawatan']; 
	$penunjang=$request->getParsedBody()['penunjang']; 
	$radiologi=$request->getParsedBody()['radiologi']; 
	$laboratorium=$request->getParsedBody()['laboratorium']; 
	$pelayanan_darah=$request->getParsedBody()['pelayanan_darah']; 
	$rehabilitasi=$request->getParsedBody()['rehabilitasi']; 
	$kamar=$request->getParsedBody()['kamar']; 
	$rawat_intensif=$request->getParsedBody()['rawat_intensif'];
	$obat=$request->getParsedBody()['obat']; 
	$obat_kronis=$request->getParsedBody()['obat_kronis']; 
	$obat_kemoterapi=$request->getParsedBody()['obat_kemoterapi']; 
	$alkes=$request->getParsedBody()['alkes']; 
	$bmhp=$request->getParsedBody()['bmhp']; 
	$sewa_alat=$request->getParsedBody()['sewa_alat']; 
	
	$pemulasaraan_jenazah=$request->getParsedBody()['pemulasaraan_jenazah']; 
	$kantong_jenazah=$request->getParsedBody()['kantong_jenazah']; 
	$peti_jenazah=$request->getParsedBody()['peti_jenazah']; 
	$plastik_erat=$request->getParsedBody()['plastik_erat']; 
	$desinfektan_jenazah=$request->getParsedBody()['desinfektan_jenazah']; 
	$mobil_jenazah=$request->getParsedBody()['mobil_jenazah']; 
	$desinfektan_mobil_jenazah=$request->getParsedBody()['desinfektan_mobil_jenazah']; 
	$covid19_status_cd=$request->getParsedBody()['covid19_status_cd']; 
	$nomor_kartu_t=$request->getParsedBody()['nomor_kartu_t']; 
	$episodes=$request->getParsedBody()['episodes']; 
    $covid19_cc_ind=$request->getParsedBody()['covid19_cc_ind']; 
    $covid19_rs_darurat_ind=$request->getParsedBody()['covid19_rs_darurat_ind']; 
    $covid19_co_insidense_ind=$request->getParsedBody()['covid19_co_insidense_ind']; 	
	
    $lab_asam_laktat=$request->getParsedBody()['lab_asam_laktat']; 	
    $lab_procalcitonin=$request->getParsedBody()['lab_procalcitonin']; 	
    $lab_crp=$request->getParsedBody()['lab_crp']; 	
    $lab_kultur=$request->getParsedBody()['lab_kultur']; 	
    $lab_d_dimer=$request->getParsedBody()['lab_d_dimer']; 	
    $lab_pt=$request->getParsedBody()['lab_pt']; 	
    $lab_aptt=$request->getParsedBody()['lab_aptt']; 	
    $lab_waktu_pendarahan=$request->getParsedBody()['lab_waktu_pendarahan']; 	
    $lab_anti_hiv=$request->getParsedBody()['lab_anti_hiv']; 	
    $lab_analisa_gas=$request->getParsedBody()['lab_analisa_gas']; 	
    $lab_albumin=$request->getParsedBody()['lab_albumin']; 	
    $rad_thorax_ap_pa=$request->getParsedBody()['rad_thorax_ap_pa']; 	

    $terapi_konvalesen=$request->getParsedBody()['terapi_konvalesen']; 	
    $akses_naat=$request->getParsedBody()['akses_naat']; 	
    $isoman_ind=$request->getParsedBody()['isoman_ind']; 	
    $bayi_lahir_status_cd=$request->getParsedBody()['bayi_lahir_status_cd']; 

    $dializer_single_use=$request->getParsedBody()['dializer_single_use']; 
	$kantong_darah=$request->getParsedBody()['kantong_darah']; 
	
	$menit_1_appearance=$request->getParsedBody()['menit_1_appearance']; 
	$menit_1_pulse=$request->getParsedBody()['menit_1_pulse']; 
	$menit_1_grimace=$request->getParsedBody()['menit_1_grimace']; 
	$menit_1_activity=$request->getParsedBody()['menit_1_activity']; 
	$menit_1_respiration=$request->getParsedBody()['menit_1_respiration']; 
	$menit_5_appearance=$request->getParsedBody()['menit_5_appearance']; 
	$menit_5_pulse=$request->getParsedBody()['menit_5_pulse']; 
	$menit_5_grimace=$request->getParsedBody()['menit_5_grimace']; 
	$menit_5_activity=$request->getParsedBody()['menit_5_activity']; 
	$menit_5_respiration=$request->getParsedBody()['menit_5_respiration']; 
	$usia_kehamilan=$request->getParsedBody()['usia_kehamilan']; 
	$gravida=$request->getParsedBody()['gravida']; 
	$partus=$request->getParsedBody()['partus']; 
	$abortus=$request->getParsedBody()['abortus']; 
	$onset_kontraksi=$request->getParsedBody()['onset_kontraksi']; 
	$delivery=$request->getParsedBody()['delivery']; 
	
	$tarif_poli_eks=$request->getParsedBody()['tarif_poli_eks']; 
	$nama_dokter=$request->getParsedBody()['nama_dokter']; 
	$kode_tarif=$request->getParsedBody()['kode_tarif']; 
	$payor_id=$request->getParsedBody()['payor_id']; 
	$payor_cd=$request->getParsedBody()['payor_cd']; 
	$cob_cd=$request->getParsedBody()['cob_cd']; 
	$coder_nik=$request->getParsedBody()['coder_nik']; 	

    $json = '{
	"metadata":{
		"method":"set_claim_data",
		"nomor_sep":"'.$nomor_sep.'"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'",
		"nomor_kartu":"'.$nomor_kartu.'",
		"tgl_masuk":"'.$tgl_masuk.'",
		"tgl_pulang":"'.$tgl_pulang.'",
		"cara_masuk":"'.$cara_masuk.'",
		"jenis_rawat":"'.$jenis_rawat.'",
		"kelas_rawat":"'.$kelas_rawat.'",
		"adl_sub_acute":"'.$adl_sub_acute.'",
		"adl_chronic":"'.$adl_chronic.'",
		"icu_indikator":"'.$icu_indikator.'",
		"icu_los":"'.$icu_los.'",
		"ventilator_hour":"'.$ventilator_hour.'",
		"ventilator": { 
			"use_ind": "'.$use_ind.'", 
			"start_dttm": "'.$start_dttm.'", 
			"stop_dttm": "'.$stop_dttm.'" 
		},
		"upgrade_class_ind":"'.$upgrade_class_ind.'",
		"upgrade_class_class":"'.$upgrade_class_class.'",
		"upgrade_class_los":"'.$upgrade_class_los.'",
		"upgrade_class_payor": "'.$upgrade_class_payor.'",
		"add_payment_pct": "'.$add_payment_pct.'",
		"birth_weight":"'.$birth_weight.'",
		"sistole":"'.$sistole.'", 
		"diastole":"'.$diastole.'",
		"discharge_status":"'.$discharge_status.'",
		"tarif_rs":{
			"prosedur_non_bedah":"'.$prosedur_non_bedah.'", 
			"prosedur_bedah":"'.$prosedur_bedah.'",
			"konsultasi":"'.$konsultasi.'",
			"tenaga_ahli":"'.$tenaga_ahli.'",
			"keperawatan":"'.$keperawatan.'",
			"penunjang":"'.$penunjang.'",
			"radiologi":"'.$radiologi.'",
			"laboratorium":"'.$laboratorium.'",
			"pelayanan_darah":"'.$pelayanan_darah.'",
			"rehabilitasi":"'.$rehabilitasi.'",
			"kamar":"'.$kamar.'",
			"rawat_intensif":"'.$rawat_intensif.'",
			"obat":"'.$obat.'",
			"obat_kronis":"'.$obat_kronis.'",
			"obat_kemoterapi":"'.$obat_kemoterapi.'",
			"alkes":"'.$alkes.'",
			"bmhp":"'.$bmhp.'",
			"sewa_alat":"'.$sewa_alat.'"
		},
		"pemulasaraan_jenazah":"'.$pemulasaraan_jenazah.'", 
		"kantong_jenazah":"'.$kantong_jenazah.'", 
		"peti_jenazah":"'.$peti_jenazah.'", 
		"plastik_erat":"'.$plastik_erat.'", 
		"desinfektan_jenazah":"'.$desinfektan_jenazah.'", 
		"mobil_jenazah":"'.$mobil_jenazah.'", 
		"desinfektan_mobil_jenazah":"'.$desinfektan_mobil_jenazah.'", 
		"covid19_status_cd":"'.$covid19_status_cd.'", 
		"nomor_kartu_t":"'.$nomor_kartu_t.'", 
		"episodes":"'.$episodes.'", 
		"covid19_cc_ind":"'.$covid19_cc_ind.'",
		"covid19_rs_darurat_ind": "'.$covid19_rs_darurat_ind.'",
		"covid19_co_insidense_ind": "'.$covid19_co_insidense_ind.'",
		"covid19_penunjang_pengurang":{
			"lab_asam_laktat" : "'.$lab_asam_laktat.'",
			"lab_procalcitonin" : "'.$lab_procalcitonin.'",
			"lab_crp" : "'.$lab_crp.'",
			"lab_kultur" : "'.$lab_kultur.'",
			"lab_d_dimer" : "'.$lab_d_dimer.'",
			"lab_pt" : "'.$lab_pt.'",
			"lab_aptt" : "'.$lab_aptt.'",
			"lab_waktu_pendarahan" : "'.$lab_waktu_pendarahan.'",
			"lab_anti_hiv" : "'.$lab_anti_hiv.'",
			"lab_analisa_gas" : "'.$lab_analisa_gas.'",
			"lab_albumin" : "'.$lab_albumin.'",
			"rad_thorax_ap_pa" : "'.$rad_thorax_ap_pa.'"
		}, 
		"terapi_konvalesen":"'.$terapi_konvalesen.'",
		"akses_naat": "'.$akses_naat.'",
		"isoman_ind":"'.$isoman_ind.'",
		"bayi_lahir_status_cd":"'.$bayi_lahir_status_cd.'",
		"dializer_single_use":"'.$dializer_single_use.'", 
		"kantong_darah":"'.$kantong_darah.'", 
		"apgar": { 
			"menit_1": { 
				"appearance":"'.$menit_1_appearance.'", 
				"pulse":"'.$menit_1_pulse.'", 
				"grimace":"'.$menit_1_grimace.'", 
				"activity":"'.$menit_1_activity.'", 
				"respiration":"'.$menit_1_respiration.'" 
			}, 
			"menit_5": { 
				"appearance":"'.$menit_5_appearance.'", 
				"pulse":"'.$menit_5_pulse.'", 
				"grimace":"'.$menit_5_grimace.'", 
				"activity":"'.$menit_5_activity.'", 
				"respiration":"'.$menit_5_respiration.'" 
			} 
		}, 
		"persalinan": { 
			"usia_kehamilan":"'.$usia_kehamilan.'", 
			"gravida":"'.$gravida.'", 
			"partus":"'.$partus.'", 
			"abortus":"'.$abortus.'", 
			"onset_kontraksi":"'.$onset_kontraksi.'", 
			"delivery":'.$delivery.'
		},
		"tarif_poli_eks":"'.$tarif_poli_eks.'",
		"nama_dokter":"'.$nama_dokter.'",
		"kode_tarif":"'.$kode_tarif.'",
		"payor_id":"'.$payor_id.'",
		"payor_cd":"'.$payor_cd.'",
		"cob_cd":"'.$cob_cd.'",
		"coder_nik":"'.$coder_nik.'"
		}}';

	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

function grouper1($request, $response, $args) {
	//$special_cmg_list = '';
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"grouper",
		"stage":"1"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';   
    
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    
	$response->write($result);	
	
	return $response;
}

function grouper2($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	$special_cmg = $request->getParsedBody()['special_cmg'];
	
	$json = '{
		"metadata":{
		"method":"grouper",
		"stage":"2"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'",
		"special_cmg":"'.$special_cmg.'"
	}}';
    
   	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

function delete_claim($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	$coder_nik = $request->getParsedBody()['coder_nik'];
	
	$json = '{
	"metadata":{
		"method":"delete_claim"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'",
		"coder_nik":"'.$coder_nik.'"
	}}';
    
   	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

function claim_print($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	
	$json = '{
	"metadata":{
		"method":"claim_print"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';
    
   	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());	

    curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);			

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

function claim_final($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	$coder_nik = $request->getParsedBody()['coder_nik'];
	
	$json = '{  
	"metadata":{  
		"method":"claim_final"
	},
	"data":{  
		"nomor_sep":"'.$nomor_sep.'",
		"coder_nik":"'.$coder_nik.'"
	}}';
   
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);
	
	$result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
    
	return $response;
}

function send_claim_individual($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];

	$json = '{
	"metadata": {
		"method":"send_claim_individual"
	},
	"data": {
		"nomor_sep":"'.$nomor_sep.'"
	}}';

	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());
   
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);
	
	$result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	
	return $response;	
}

function reedit_claim($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];	
	
	$json = '{
	"metadata": {
		"method":"reedit_claim"
	},
	"data": {
		"nomor_sep":"' .$nomor_sep.'"
	}}';
		  
	$json = mc_encrypt ($json, getKey());
	$ch = curl_init(getUrlWS());

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);
	
	$result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	
	return $response;
}

function pull_claim($request, $response, $args) {
	$json = '{
	"metadata": {
		"method":"pull_claim"
	},
	"data": {
		"start_dt":"'.$request->getParsedBody()['stat_dt'].'",
		"stop_dt":"'.$request->getParsedBody()['stop_dt'].'",
		"jenis_rawat":"'.$request->getParsedBody()['jenis_rawat'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());		

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    //if ($request->getParsedBody()['krip'] == 'false') {
	$result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	//}else{$response->write($result);}
	return $response;	
}

function get_claim_data($request,$response, $args) {
	$json = '{
	"metadata": {
		"method":"get_claim_data"
	},
	"data": {
		"nomor_sep":"'.$request->getParsedBody()['nomor_sep'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	
	return $response;
}

function set_diagnose_data($request,$response, $args) {
	$json = '{
	"metadata": {
		"method":"set_claim_data",
		"nomor_sep":"'.$request->getParsedBody()['nomor_sep'].'"
	},
	"data": {
		"payor_id": "'.$request->getParsedBody()['payor_id'].'",
		"diagnosa": "'.$request->getParsedBody()['diagnosa'].'",
		"coder_nik": "'.$request->getParsedBody()['coder_nik'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	
	return $response;
}

function set_procedure_data($request,$response, $args) {
	$json = '{
	"metadata": {
		"method":"set_claim_data",
		"nomor_sep":"'.$request->getParsedBody()['nomor_sep'].'"
	},
	"data": {
		"payor_id": "'.$request->getParsedBody()['payor_id'].'",
		"procedure": "'.$request->getParsedBody()['procedure'].'",
		"coder_nik": "'.$request->getParsedBody()['coder_nik'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	
	return $response;
}

function get_diagnose_inagroupper($request,$response, $args) {
	$json = '{
	"metadata": {
		"method":"search_diagnosis_inagrouper"
	},
	"data": {
		"keyword": "'.$request->getParsedBody()['keyword'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	
	return $response;
}

function get_procedure_inagroupper($request,$response, $args) {
	$json = '{
	"metadata": {
		"method":"search_procedures_inagrouper"
	},
	"data": {
		"keyword": "'.$request->getParsedBody()['keyword'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	
	return $response;
}

function set_sitb_validate($request,$response, $args) {
	$json = '{
	"metadata": {
		"method":"sitb_validate"
	},
	"data": {
		"nomor_sep": "'.$request->getParsedBody()['nomor_sep'].'", 
		"nomor_register_sitb": "'.$request->getParsedBody()['nomor_register_sitb'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);
    $response->write($result);	
	
	return $response;
}

function set_sitb_invalidate($request,$response, $args) {
	$json = '{
	"metadata": {
		"method":"sitb_invalidate"
	},
	"data": {
		"nomor_sep": "'.$request->getParsedBody()['nomor_sep'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	
	
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace ('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace ('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt (getKey(), $result);			    
    $response->write($result);	
	
	return $response;
}

function mc_encrypt($data, $key) {
	$key = hex2bin($key);
	if (mb_strlen($key, "8bit") !== 32) {
		throw new Exception("Needs a 256-bit key!");
	}
	$iv_size = openssl_cipher_iv_length("aes-256-cbc");

	$iv = openssl_random_pseudo_bytes($iv_size);
	$encrypted = openssl_encrypt($data,"aes-256-cbc",$key,OPENSSL_RAW_DATA,$iv );

	$signature = mb_substr(hash_hmac("sha256",$encrypted,$key,true),0,10,"8bit");
	$encoded = chunk_split(base64_encode($signature.$iv.$encrypted));
	return $encoded;
}

function mc_decrypt($strkey, $str) {
	$key = hex2bin($strkey);

	///	check key length, must be 256 bit or 32 bytes 
	if (mb_strlen($key, "8bit") !== 32) {
		throw new Exception("Needs a 256-bit key!");
	}

	$iv_size = openssl_cipher_iv_length("aes-256-cbc");
	$decoded = base64_decode($str);
	$signature = mb_substr($decoded,0,10,"8bit"); 
	$iv = mb_substr($decoded,10,$iv_size,"8bit");
	$encrypted = mb_substr($decoded,$iv_size+10,NULL,"8bit");
	$calc_signature = mb_substr(hash_hmac("sha256",$encrypted,$key,true),0,10,"8bit");
	if(!mc_compare($signature,$calc_signature)) {
		return "SIGNATURE_NOT_MATCH"; /// signature doesn't match
	}

	$decrypted = openssl_decrypt($encrypted,"aes-256-cbc",$key,OPENSSL_RAW_DATA,$iv);
	return $decrypted;
}

function mc_compare($a, $b) {
	///	compare individually to prevent timing attacks
	///	compare length
	if (strlen($a) !== strlen($b)) return false;

	///	compare individual 
	$result = 0;

	for($i = 0; $i < strlen($a); $i ++) { 
		$result |= ord($a[$i]) ^ ord($b[$i]);
	}

	return $result == 0;
}

function search_diagnosis($request,$response, $args) {
	$json = '{
	"metadata":{
		"method":"search_diagnosis"
	},
	"data":{
		"keyword":"'.$request->getParsedBody()['keyword'].'"
	}}';
	
	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;
}

function search_procedures($request,$response, $args) {
	$json = '{
	"metadata":{
		"method":"search_procedures"
	},
	"data":{
		"keyword":"'.$request->getParsedBody()['keyword'].'"
	}}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;   
}

function generate_claim_number($request,$response, $args) {
	$json = '{
	"metadata":{
		"method":"generate_claim_number"
	},
	"data":{
		"payor_id":"'.$request->getParsedBody()['payor_id'].'"
	}}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;   
}

function file_upload($request,$response, $args) {
	$json = '{
	"metadata":{
		"method":"file_upload",
		"nomor_sep":"'.$request->getParsedBody()['nomor_sep'].'",
		"file_class":"'.$request->getParsedBody()['file_class'].'",
		"file_name":"'.$request->getParsedBody()['file_name'].'"
	},
	"data":"'.$request->getParsedBody()['data'].'"
	}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;   
}

function file_delete($request,$response, $args) {
	$json = '{
	"metadata":{
		"method":"file_delete"
	},
	"data":{
		 "nomor_sep":"'.$request->getParsedBody()['nomor_sep'].'",
		 "file_id":"'.$request->getParsedBody()['file_id'].'"
	}}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;   
}

function file_get($request,$response, $args) {
	$json = '{
	"metadata":{
		"method":"file_get"
	},
	"data":{
		 "nomor_sep":"'.$request->getParsedBody()['nomor_sep'].'"
	}}';

	$json = mc_encrypt ($json, getKey());

	$ch = curl_init(getUrlWS());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);

    $result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);			    
    $response->write($result);	

	return $response;
}

function idrg_diagnosa_set($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	$diagnosa  = $request->getParsedBody()['diagnosa']; // format: KODE1#KODE2#...

	$json = '{
	"metadata":{
		"method":"idrg_diagnosa_set",
		"nomor_sep":"'.$nomor_sep.'"
	},
	"data":{
		"diagnosa":"'.$diagnosa.'"
	}}';

	$json = mc_encrypt($json, getKey());
	$ch = curl_init(getUrlWS());
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
	$result = curl_exec($ch);
	curl_close($ch);

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);
	$response->write($result);
	return $response;
}

function idrg_procedure_set($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];
	$procedure = $request->getParsedBody()['procedure']; // contoh: "88.72+2#99.10"

	$json = '{
	"metadata":{
		"method":"idrg_procedure_set",
		"nomor_sep":"'.$nomor_sep.'"
	},
	"data":{
		"procedure":"'.$procedure.'"
	}}';

	$json = mc_encrypt($json, getKey());
	$ch = curl_init(getUrlWS());
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
	$result = curl_exec($ch);
	curl_close($ch);

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);
	$response->write($result);
	return $response;
}

function grouper_idrg($request, $response, $args) {
	$nomor_sep = $request->getParsedBody()['nomor_sep'];

	$json = '{
	"metadata":{
		"method":"grouper",
		"stage":"1",
		"grouper":"idrg"
	},
	"data":{
		"nomor_sep":"'.$nomor_sep.'"
	}}';

	$json = mc_encrypt($json, getKey());
	$ch = curl_init(getUrlWS());
	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
	$result = curl_exec($ch);
	curl_close($ch);

	$result = str_replace('----BEGIN ENCRYPTED DATA----', '', $result);
	$result = str_replace('----END ENCRYPTED DATA----', '', $result);
	$result = mc_decrypt(getKey(), $result);
	$response->write($result);
	return $response;
}

function new_pacs_worklist($request, $response, $args) {
	$hl7 = array(
		'accessionNumber'   => $request->getParsedBody()['procedureStepID'],
		'patientName'       => $request->getParsedBody()['patientName'],
		'patientID'         => $request->getParsedBody()['patientID'],
		'patientBirthDate'  => $request->getParsedBody()['patientBirthDate'],
		'patientGender'     => $request->getParsedBody()['patientGender'],
		'studyInstanceUID'  => $request->getParsedBody()['procedureStepID'],	
		'modality'          => $request->getParsedBody()['modalityID'],

		// SCHEDULE
		'stationAETitle'            => 'DCM4CHEE',
		'procedureStepStartDate'    => $request->getParsedBody()['procedureStepStartDate'],
		'procedureStepStartTime'    => $request->getParsedBody()['procedureStepStartTime'],
		'performingPhysicianName'   => $request->getParsedBody()['performingPhysicianName'],
		'procedureStepDescription'  => $request->getParsedBody()['procedureStepDescription'],
		'procedureStepID'           => $request->getParsedBody()['procedureStepID'],
		'stationName'               => '',
		'procedureStepStatus'       => 'XO(SC)'
	);

	$payload =  json_encode(
                array(
                    'username' => 'admin',      
                    'password' => md5('admin'),
                    'hl7' => $hl7,
                    'debug'=> 0
                )
            );
	
	$ch = curl_init(getUrlPacs());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);
	
	$response->write($result);	

	return $response;
}

function status_pacs_worklist($request, $response, $args) {
	$payload =  json_encode(
                array(
                    'username' => 'admin',      
                    'password' => md5('admin'),
                    'accessionNumber' => $request->getParsedBody()['accessionNumber']
                )
            );
	
	$ch = curl_init(getUrlPacs());	

	curl_setopt($ch, CURLOPT_POST, 1);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

	$result = curl_exec($ch);
	curl_close($ch);
	
	$response->write($result);	

	return $response;
}

function siranap_bed_monitoring_new($request, $response, $args) {
	$xrsid = $request->getParsedBody()['xrsid'];
	$xpass = md5($request->getParsedBody()['xpass']);

	$url = "http://sirs.yankes.kemkes.go.id/sirsservice/ranap";
	
	$xmlStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xml>\n";
	$xmlStr .= "<data>\n";
	$xmlStr .= "<kode_ruang>".$request->getParsedBody()['kode_ruang']."</kode_ruang>\n";
	$xmlStr .= "<tipe_pasien>".$request->getParsedBody()['tipe_pasien']."</tipe_pasien>\n";
	$xmlStr .= "<total_TT>".$request->getParsedBody()['total_tt']."</total_TT>\n";
	$xmlStr .= "<terpakai_male>".$request->getParsedBody()['terpakai_male']."</terpakai_male>\n";
	$xmlStr .= "<terpakai_female>".$request->getParsedBody()['terpakai_female']."</terpakai_female>\n";
	$xmlStr .= "<kosong_male>".$request->getParsedBody()['kosong_male']."</kosong_male>\n";
	$xmlStr .= "<kosong_female>".$request->getParsedBody()['kosong_female']."</kosong_female>\n";
	$xmlStr .= "<waiting>0</waiting>\n";
	$xmlStr .= "<tgl_update>".$request->getParsedBody()['tgl_update']."</tgl_update>\n";
	$xmlStr .= "</data>\n";
	$xmlStr .="</xml>\n";
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);  
	curl_setopt($curl, CURLOPT_HTTPHEADER, 
		array(
			"X-rs-id: $xrsid",
			"X-pass: $xpass",
			"Content-Type:text/xml"
		) 
	); 
	curl_setopt($curl, CURLOPT_NOBODY, false);
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true); 
	curl_setopt($curl, CURLOPT_POST, 1);
	curl_setopt($curl, CURLOPT_POSTFIELDS, $xmlStr);
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function siranap_bed_monitoring_delete($request, $response, $args) {
	$xrsid = $request->getParsedBody()['xrsid'];
	$xpass = md5($request->getParsedBody()['xpass']);
    $tipe_pasien = $request->getParsedBody()['tipe_pasien'];
	$kode_ruang = $request->getParsedBody()['kode_ruang'];
	
	$url = "http://sirs.yankes.kemkes.go.id/sirsservice/sisrute/hapusdata/$xrsid/$tipe_pasien/$kode_ruang";
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);  
	curl_setopt($curl, CURLOPT_HTTPHEADER, 
		array(
			"X-rs-id: $xrsid",
			"X-pass: $xpass",
			"Content-Type:text/xml"
		) 
	); 
	curl_setopt($curl, CURLOPT_NOBODY, false);
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true); 
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function sisrute_get_rujukan($request, $response, $args) {
	$id = $request->getParsedBody()['consid'];
	$pass = md5($request->getParsedBody()['salt']);
	$nomor = $request->getParsedBody()['nomor'];
	$tanggal = $request->getParsedBody()['tanggal'];
   
	//Get Timestamp
	$dt = new DateTime(null, new DateTimeZone("UTC"));
	$timestamp = $dt->getTimestamp();
   
	// Generate Signature
	$key = $id."&".$timestamp;					
	$signature = base64_encode(hash_hmac("sha256", utf8_encode($key), utf8_encode($pass), true));
	if ($nomor != "" && $tanggal != "")
		$url = "http://api.dvlp.sisrute.kemkes.go.id/rujukan?create=true&nomor=$nomor&tanggal=$tanggal";
	else if ($nomor == "" && $tanggal != "")
		$url = "http://api.dvlp.sisrute.kemkes.go.id/rujukan?create=true&tanggal=$tanggal";
	else
		$url = "http://api.dvlp.sisrute.kemkes.go.id/rujukan?create=true&nomor=$nomor";
	
	$method = "GET";
	$headers = [
		"X-cons-id: ".$id,
		"X-Timestamp: ".$timestamp,
		"X-signature: ".$signature,
		"Accept: application/json"      
	];
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_HEADER, false);
	curl_setopt($curl, CURLOPT_CUSTOMREQUEST, $method);
	
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, false);
	curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
	curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function sisrute_get_diagnosa($request, $response, $args) {
	$id = $request->getParsedBody()['consid'];
	$pass = md5($request->getParsedBody()['salt']);
	$kode = $request->getParsedBody()['kode'];
   
	//Get Timestamp
	$dt = new DateTime(null, new DateTimeZone("UTC"));
	$timestamp = $dt->getTimestamp();
   
	// Generate Signature
	$key = $id."&".$timestamp;					
	$signature = base64_encode(hash_hmac("sha256", utf8_encode($key), utf8_encode($pass), true));
	
	$url = "http://api.dvlp.sisrute.kemkes.go.id/referensi/diagnosa?query=$kode";
		
	$method = "GET";
	$headers = [
		"X-cons-id: ".$id,
		"X-Timestamp: ".$timestamp,
		"X-signature: ".$signature,
		"Accept: application/json"      
	];
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_HEADER, false);
	curl_setopt($curl, CURLOPT_CUSTOMREQUEST, $method);
	
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, false);
	curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
	curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function sisrute_get_alasan($request, $response, $args) {
	$id = $request->getParsedBody()['consid'];
	$pass = md5($request->getParsedBody()['salt']);
	$kode = $request->getParsedBody()['kode'];
   
	//Get Timestamp
	$dt = new DateTime(null, new DateTimeZone("UTC"));
	$timestamp = $dt->getTimestamp();
   
	// Generate Signature
	$key = $id."&".$timestamp;					
	$signature = base64_encode(hash_hmac("sha256", utf8_encode($key), utf8_encode($pass), true));
	
	$url = "http://api.dvlp.sisrute.kemkes.go.id/referensi/alasanrujukan?query=$kode";
	
	$method = "GET";
	$headers = [
		"X-cons-id: ".$id,
		"X-Timestamp: ".$timestamp,
		"X-signature: ".$signature,
		"Accept: application/json"      
	];
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_HEADER, false);
	curl_setopt($curl, CURLOPT_CUSTOMREQUEST, $method);
	
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, false);
	curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
	curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function sisrute_get_faskes($request, $response, $args) {
	$id = $request->getParsedBody()['consid'];
	$pass = md5($request->getParsedBody()['salt']);
	$kode = $request->getParsedBody()['kode'];
   
	//Get Timestamp
	$dt = new DateTime(null, new DateTimeZone("UTC"));
	$timestamp = $dt->getTimestamp();
   
	// Generate Signature
	$key = $id."&".$timestamp;					
	$signature = base64_encode(hash_hmac("sha256", utf8_encode($key), utf8_encode($pass), true));
	
	$url = "http://api.dvlp.sisrute.kemkes.go.id/referensi/faskes?query=$kode";
		
	$method = "GET";
	$headers = [
		"X-cons-id: ".$id,
		"X-Timestamp: ".$timestamp,
		"X-signature: ".$signature,
		"Accept: application/json"      
	];
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_HEADER, false);
	curl_setopt($curl, CURLOPT_CUSTOMREQUEST, $method);
	
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, false);
	curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
	curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function sisrute_set_rujukan($request, $response, $args) {
	$id = $request->getParsedBody()['consid'];
	$pass = md5($request->getParsedBody()['salt']);
   
	$json = '{
	"PASIEN": {
		"NORM": "'.$request->getParsedBody()['NORM'].'",
		"NIK": "'.$request->getParsedBody()['NIK'].'",
		"NO_KARTU_JKN": "'.$request->getParsedBody()['NO_KARTU_JKN'].'",
		"NAMA": "'.$request->getParsedBody()['NAMA'].'",
		"JENIS_KELAMIN": "'.$request->getParsedBody()['JENIS_KELAMIN'].'",
		"TANGGAL_LAHIR": "'.$request->getParsedBody()['TANGGAL_LAHIR'].'",
		"TEMPAT_LAHIR": "'.$request->getParsedBody()['TEMPAT_LAHIR'].'",
		"ALAMAT": "'.$request->getParsedBody()['ALAMAT'].'",
		"KONTAK": "'.$request->getParsedBody()['KONTAK'].'"
	},
	"RUJUKAN": {
		"JENIS_RUJUKAN": "'.$request->getParsedBody()['JENIS_RUJUKAN'].'",
		"TANGGAL": "'.$request->getParsedBody()['TANGGAL'].'",
		"FASKES_TUJUAN": "'.$request->getParsedBody()['FASKES_TUJUAN'].'",
		"ALASAN": "'.$request->getParsedBody()['ALASAN'].'",
		"ALASAN_LAINNYA": "'.$request->getParsedBody()['ALASAN_LAINNYA'].'",
		"DIAGNOSA": "'.$request->getParsedBody()['DIAGNOSA'].'",
		"DOKTER": {
			"NIK": "'.$request->getParsedBody()['NIK_DOKTER'].'",
			"NAMA": "'.$request->getParsedBody()['NAMA_DOKTER'].'"
		},
		"PETUGAS": {
			"NIK": "'.$request->getParsedBody()['NIK_PETUGAS'].'",
			"NAMA": "'.$request->getParsedBody()['NAMA_PETUGAS'].'"
		}
	},
	"KONDISI_UMUM": {
		"ANAMNESIS_DAN_PEMERIKSAAN_FISIK": "'.$request->getParsedBody()['ANAMNESIS_DAN_PEMERIKSAAN_FISIK'].'",
		"KESADARAN": "'.$request->getParsedBody()['KESADARAN'].'",
		"TEKANAN_DARAH": "'.$request->getParsedBody()['TEKANAN_DARAH'].'",
		"FREKUENSI_NADI": "'.$request->getParsedBody()['FREKUENSI_NADI'].'",
		"SUHU": "'.$request->getParsedBody()['SUHU'].'",
		"PERNAPASAN": "'.$request->getParsedBody()['PERNAPASAN'].'",
		"KEADAAN_UMUM": "'.$request->getParsedBody()['KEADAAN_UMUM'].'",
		"NYERI": "'.$request->getParsedBody()['NYERI'].'",
		"ALERGI": "'.$request->getParsedBody()['ALERGI'].'"
	},
	"PENUNJANG": {
		"LABORATORIUM": "'.$request->getParsedBody()['LABORATORIUM'].'",
		"RADIOLOGI": "'.$request->getParsedBody()['RADIOLOGI'].'",
		"TERAPI_ATAU_TINDAKAN": "'.$request->getParsedBody()['TERAPI_ATAU_TINDAKAN'].'"
	}
}';

   //Get Timestamp
	$dt = new DateTime(null, new DateTimeZone("UTC"));
	$timestamp = $dt->getTimestamp();
   
	// Generate Signature
	$key = $id."&".$timestamp;					
	$signature = base64_encode(hash_hmac("sha256", utf8_encode($key), utf8_encode($pass), true));
	
	$url = "http://api.dvlp.sisrute.kemkes.go.id/rujukan";
		
	$method = $request->getParsedBody()['method'];
	$headers = [
		"X-cons-id: ".$id,
		"X-Timestamp: ".$timestamp,
		"X-signature: ".$signature,
		"Accept: application/json",
		"Content-Type: application/json",
		"Content-length: ".strlen($json)
	];
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_HEADER, false);
	curl_setopt($curl, CURLOPT_CUSTOMREQUEST, $method);
	curl_setopt($curl, CURLOPT_POSTFIELDS, $json);
		
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, false);
	curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
	curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function sisrute_batal_rujukan($request, $response, $args) {
	$id = $request->getParsedBody()['consid'];
	$pass = md5($request->getParsedBody()['salt']);
	$kode = $request->getParsedBody()['kode'];
   
	$json = '{
	"PETUGAS": {
			"NIK": "'.$request->getParsedBody()['NIK_PETUGAS'].'",
			"NAMA": "'.$request->getParsedBody()['NAMA_PETUGAS'].'"
		}
}';

	//Get Timestamp
	$dt = new DateTime(null, new DateTimeZone("UTC"));
	$timestamp = $dt->getTimestamp();
   
	// Generate Signature
	$key = $id."&".$timestamp;					
	$signature = base64_encode(hash_hmac("sha256", utf8_encode($key), utf8_encode($pass), true));
	
	$url = "http://api.dvlp.sisrute.kemkes.go.id/rujukan/batal/$kode";
	$method = "PUT";
	$headers = [
		"X-cons-id: ".$id,
		"X-Timestamp: ".$timestamp,
		"X-signature: ".$signature,
		"Accept: application/json",
		"Content-Type: application/json",
		"Content-length: ".strlen($json)
	];
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_HEADER, false);
	curl_setopt($curl, CURLOPT_CUSTOMREQUEST, $method);
	curl_setopt($curl, CURLOPT_POSTFIELDS, $json);
		
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, false);
	curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
	curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function sisrute_jawab_rujukan($request, $response, $args) {	
	$id = $request->getParsedBody()['consid'];
	$pass = md5($request->getParsedBody()['salt']);
	$kode = $request->getParsedBody()['kode'];
   
	//Get Timestamp
	$dt = new DateTime(null, new DateTimeZone("UTC"));
	$timestamp = $dt->getTimestamp();
   
	// Generate Signature
	$key = $id."&".$timestamp;					
	$signature = base64_encode(hash_hmac("sha256", utf8_encode($key), utf8_encode($pass), true));
	
	$url = "http://api.dvlp.sisrute.kemkes.go.id/rujukan/jawab/$kode";
	$method = "PUT";
	$headers = [
		"X-cons-id: ".$id,
		"X-Timestamp: ".$timestamp,
		"X-signature: ".$signature,
		"Accept: application/json"      
	];
	
	$curl = curl_init(); 
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_HEADER, false);
	curl_setopt($curl, CURLOPT_CUSTOMREQUEST, $method);
	
	curl_setopt($curl, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($curl, CURLOPT_SSL_VERIFYHOST, false);
	curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
	curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
	
	$str = curl_exec($curl);  
	curl_close($curl);  
	
	$response->write($str);	

	return $response;
}

function dukcapil_get_nik_rshm($request, $response, $args) {
	$nik=$request->getParsedBody()['nik'];
	$user_id=$request->getParsedBody()['user_id'];
	$password=$request->getParsedBody()['password'];
	$host=$request->getParsedBody()['host'];
	
	$json = '{
    "nik":"'.$nik.'",
    "user_id":"'.$user_id.'",
    "password":"'.$password.'",
    "ip_user":"'.$host.'"
}';

	$ch = curl_init();
	curl_setopt($ch, CURLOPT_URL, "http://172.16.160.43:8080/dukcapil/get_json/12/rsu_haji_medan_12/call_nik");
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($ch, CURLOPT_HTTPHEADER, array("Content-Type: application/json", "Accept: application/json"));
	curl_setopt($ch, CURLOPT_POST, true);
	curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
	$result = curl_exec($ch);
	curl_close($ch);

    $response->write($result);	

	return $response;  
}

function dukcapil_get_nik_tarakan($request, $response, $args) {
	$nik = $request->getParsedBody()['nik'];
	$hash = MD5(date("dmY"));
	$key =  MD5("TARAKAN".$hash."RSUD");
	$url = "http://10.0.0.20/rsudtarakan/xml.jsp?usernm=wsrsudtarakan&pass=GwWSRSuDT4rakan&app=SILaporLahir&pget=Kelahiran&pusr=admin&proc=GETNIK&nik=$nik&pkey=$key";
	if($url == '') return "EMPTY";

	$xml = simplexml_load_file($url);
	
	$response->write($xml);	

	return $response;  
}

function vklaim_decrypt($request, $response, $args) {
	$key = $request->getParsedBody()['key']; //265235aJB1A86DF1630236863
	$string = $request->getParsedBody()['string']; //mkI8uEuwT9J5N53UN45wxKYSYnJs228NJ/5QoiEN8+9ojhYqUHKyueOiRwzZg3xsW+mJ8oP86SJyYOohX04U0Zr10XONdHb72PHjZV+kW9TWj3bvYDqEFA0opcZOawi1IQ0bmaAw5AAIV/W3MD09++sBVxAeqcTJeViEHhLzS4OujisoLgcQTSPFR2AQwElHYcU96lsDro7zRPUKC3htXxexkadnBK1HTp/JykxB+hxr6rOhSoOCCJFCjMcDep+aQdNjXvFK+XfXQMcdv9t7e2LCLEKTRGI9oekEtqfgcu1Eobb5VzxzAXogdBeBeWLxtLAG5nnMXPw7ZmSUwwAcCaCm1vJfxEHUuPS8X90H1cFmP/FFtgk9YItrIWMLMVROSt48HOxlZ+JdqaACZVAriIrZ1Vhk/XFgS1g+64gH+ySizfjpDRHH2chUOeGQiUsINrPvsONxuebGJSij2Z4oZYZ9Cghn5fFj7SIbPxFUszJzOCyeZYc8DdH/guVeCL6NO2bxYeMKm8KV/6bPF8P748C5s1a87lA0hG1KjMYdnR4X8Cc79xZ3lWfDi8RjaCCRLk7bS9DaOiO/Bq5hQpd2teF1nQUycF8v2E00mI+ip+WIBdWL7kCuglSrYEemSxh3naVhwPm0+WcxJMuioG7lp8B7sB2cKCUuPL5KY5DvSeQdj9wpYJtNudIUThwB8Ad2Iei64qrwshKnSEIGbqZJmOYvX8g07EevE6aZwvxoj49i6MYNl5Z6SG011U2PMaSqif84el8i1g7cpuYXWSqpa0MBV29RKBxzZlkR1NRT/idOZx118u/bkzMZLAaDdOffSF+QTzDVt7irdR74oOjVpd7vCHUy4zNEpy5v6ylP6T8=
	$encrypt_method = 'AES-256-CBC';

	//$xxx = '26523'.'5aJB1A86DF'.'1630404401';
	
	// hash
	$key_hash = hex2bin(hash('sha256', $key));

	// iv - encrypt method AES-256-CBC expects 16 bytes - else you will get a warning
	$iv = substr(hex2bin(hash('sha256', $key)), 0, 16);
	
	$output = openssl_decrypt(base64_decode($string), $encrypt_method, $key_hash, OPENSSL_RAW_DATA, $iv);

	$response->write(vklaim_decompress($output));
	
	return $response;
}

function vklaim_decompress($string) {
	return \LZCompressor\LZString::decompressFromEncodedURIComponent($string);
}
?>