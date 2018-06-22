var pollingSpeed = 250;//in ms
var fadeSpeed = 3500; // in ms
var fadeAnimationSpeed = 500;
var informationBarSpeed = 6000;
document.documentElement.style.overflow = 'hidden';  // firefox, chrome

function updateInformation()
{
	$.get('http://127.0.0.1:8081/getBracketValues', function (json) {
		//Winners Semi
		$('#DWSAp1name').text(json.DWSA.Player1);
		$('#DWSAp2name').text(json.DWSA.Player2);
		$('#DWSAp1score').text(json.DWSA.Player1score);
		$('#DWSAp2score').text(json.DWSA.Player2score);

		$('#DWSBp1name').text(json.DWSB.Player1);
		$('#DWSBp2name').text(json.DWSB.Player2);
		$('#DWSBp1score').text(json.DWSB.Player1score);
		$('#DWSBp2score').text(json.DWSB.Player2score);
		//Winners Finals
		$('#DWFp1name').text(json.DWF.Player1);
		$('#DWFp2name').text(json.DWF.Player2);
		$('#DWFp1score').text(json.DWF.Player1score);
		$('#DWFp2score').text(json.DWF.Player2score);
		//Grand Finals
		$('#DGFp1name').text(json.DGF.Player1);
		$('#DGFp2name').text(json.DGF.Player2);
		$('#DGFp1score').text(json.DGF.Player1score);
		$('#DGFp2score').text(json.DGF.Player2score);
		//Bracket Reset
		$('#DBRp1name').text(json.DBR.Player1);
		$('#DBRp2name').text(json.DBR.Player2);
		$('#DBRp1score').text(json.DBR.Player1score);
		$('#DBRp2score').text(json.DBR.Player2score);
		//Losers Finals
		$('#DLFp1name').text(json.DLF.Player1);
		$('#DLFp2name').text(json.DLF.Player2);
		$('#DLFp1score').text(json.DLF.Player1score);
		$('#DLFp2score').text(json.DLF.Player2score);
		//Losers Semi
		$('#DLSp1name').text(json.DLSF.Player1);
		$('#DLSp2name').text(json.DLSF.Player2);
		$('#DLSp1score').text(json.DLSF.Player1score);
		$('#DLSp2score').text(json.DLSF.Player2score);
		//Losers Quarters
		$('#DLQAp1name').text(json.DLQA.Player1);
		$('#DLQAp2name').text(json.DLQA.Player2);
		$('#DLQAp1score').text(json.DLQA.Player1score);
		$('#DLQAp2score').text(json.DLQA.Player2score);

		$('#DLQBp1name').text(json.DLQB.Player1);
		$('#DLQBp2name').text(json.DLQB.Player2);
		$('#DLQBp1score').text(json.DLQB.Player1score);
		$('#DLQBp2score').text(json.DLQB.Player2score);

	}, 'json');
	//poll stuff periodically
	setTimeout("updateInformation()", pollingSpeed);
}
$(document).ready(function() {
	updateInformation();
});