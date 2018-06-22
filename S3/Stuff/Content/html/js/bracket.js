var pollingSpeed = 250;//in ms
var fadeSpeed = 3500; // in ms
var fadeAnimationSpeed = 500;
var informationBarSpeed = 6000;
document.documentElement.style.overflow = 'hidden';  // firefox, chrome

function updateInformation()
{
	$.get('http://127.0.0.1:8081/getBracketValues', function (json) {
		//Winners Semi
		$('#WSAp1name').text(json.WSA.Player1);
		$('#WSAp2name').text(json.WSA.Player2);
		$('#WSAp1score').text(json.WSA.Player1score);
		$('#WSAp2score').text(json.WSA.Player2score);

		$('#WSBp1name').text(json.WSB.Player1);
		$('#WSBp2name').text(json.WSB.Player2);
		$('#WSBp1score').text(json.WSB.Player1score);
		$('#WSBp2score').text(json.WSB.Player2score);
		//Winners Finals
		$('#WFp1name').text(json.WF.Player1);
		$('#WFp2name').text(json.WF.Player2);
		$('#WFp1score').text(json.WF.Player1score);
		$('#WFp2score').text(json.WF.Player2score);
		//Grand Finals
		$('#GFp1name').text(json.GF.Player1);
		$('#GFp2name').text(json.GF.Player2);
		$('#GFp1score').text(json.GF.Player1score);
		$('#GFp2score').text(json.GF.Player2score);
		//Bracket Reset
		$('#BRp1name').text(json.BR.Player1);
		$('#BRp2name').text(json.BR.Player2);
		$('#BRp1score').text(json.BR.Player1score);
		$('#BRp2score').text(json.BR.Player2score);
		//Losers Finals
		$('#LFp1name').text(json.LF.Player1);
		$('#LFp2name').text(json.LF.Player2);
		$('#LFp1score').text(json.LF.Player1score);
		$('#LFp2score').text(json.LF.Player2score);
		//Losers Semi
		$('#LSp1name').text(json.LSF.Player1);
		$('#LSp2name').text(json.LSF.Player2);
		$('#LSp1score').text(json.LSF.Player1score);
		$('#LSp2score').text(json.LSF.Player2score);
		//Losers Quarters
		$('#LQAp1name').text(json.LQA.Player1);
		$('#LQAp2name').text(json.LQA.Player2);
		$('#LQAp1score').text(json.LQA.Player1score);
		$('#LQAp2score').text(json.LQA.Player2score);

		$('#LQBp1name').text(json.LQB.Player1);
		$('#LQBp2name').text(json.LQB.Player2);
		$('#LQBp1score').text(json.LQB.Player1score);
		$('#LQBp2score').text(json.LQB.Player2score);
		//Losers Top8
		$('#L8Ap1name').text(json.L8A.Player1);
		$('#L8Ap2name').text(json.L8A.Player2);
		$('#L8Ap1score').text(json.L8A.Player1score);
		$('#L8Ap2score').text(json.L8A.Player2score);

		$('#L8Bp1name').text(json.L8B.Player1);
		$('#L8Bp2name').text(json.L8B.Player2);
		$('#L8Bp1score').text(json.L8B.Player1score);
		$('#L8Bp2score').text(json.L8B.Player2score);

	}, 'json');
	//poll stuff periodically
	setTimeout("updateInformation()", pollingSpeed);
}
$(document).ready(function() {
	updateInformation();
});