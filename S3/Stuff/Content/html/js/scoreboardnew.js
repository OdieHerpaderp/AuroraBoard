var pollingSpeed = 250;//in ms
var fadeSpeed = 3500; // in ms
var fadeAnimationSpeed = 500;
var informationBarSpeed = 6000;
document.documentElement.style.overflow = 'hidden';  // firefox, chrome

function updateInformation()
{
	$.get('http://127.0.0.1:8081/getCurrentValues', function (json) {
		//player names
		$('#player1Text').text(json.Player1.name);
		$('#player2Text').text(json.Player2.name);
		// play with font widths
		if($('#player1Text').text().length > 20) {
			$('#player1Text').css("font-stretch", "condensed");
		}
		else if($('#player1Text').text().length < 14) {
			$('#player1Text').css("font-stretch", "ultra-expanded");
		}
		else {
			$('#player1Text').css("font-stretch", "normal");
		}
		
		if($('#player2Text').text().length > 20) {
			$('#player2Text').css("font-stretch", "condensed");
		}
		else if($('#player2Text').text().length < 14) {
			$('#player2Text').css("font-stretch", "ultra-expanded");
		}
		else {
			$('#player2Text').css("font-stretch", "normal");
		}
		//scores
		$('#player1Score').text(json.Player1.score.toString());
		$('#player2Score').text(json.Player2.score.toString());
		//sponsors
		$("#sponsorImgP1").attr('src', json.Player1.SponsorIcon);
		$("#sponsorImgP2").attr('src', json.Player2.SponsorIcon);
		//topbar
		$("#matchType").text(json.matchType);
		$("#round").text(json.round);
		$("#tournamentName").text(json.tournamentName);
		//information bar
		$("#casterText").text(json.caster);
		$("#streamerText").text(json.streamer);

		//playercamtext
		$('#player1streamname').text(json.Player1.name);
		$('#player2streamname').text(json.Player2.name);
		//character images
		if(json.iconStyle == "LEGACY")
		{
			$('#characterIMGP1').attr('src', "img/characters/" + json.Player1.character.icon);
			$('#characterIMGP2').attr('src', "img/characters/" + json.Player2.character.icon);
			$('#characterIMGP1').css("width", "106px");
			$('#characterIMGP1').css("height", "74px");
			$('#characterIMGP2').css("width", "106px");
			$('#characterIMGP2').css("height", "74px");
			$('#characterIMGP1').css("left", "-9px");
			$('#characterIMGP2').css("right", "-9px");
		}
		else if(json.iconStyle == "LTESTOCK")
		{
			$('#characterIMGP1').attr('src', "img/ltestock/" + json.Player1.character.icon);
			$('#characterIMGP2').attr('src', "img/ltestock/" + json.Player2.character.icon);
			$('#characterIMGP1').css("width", "74px");
			$('#characterIMGP1').css("height", "74px");
			$('#characterIMGP2').css("width", "74px");
			$('#characterIMGP2').css("height", "74px");
			$('#characterIMGP1').css("left", "-6px");
			$('#characterIMGP2').css("right", "-6px");
		}
		else if(json.iconStyle == "LTE2HD")
		{
			$('#characterIMGP1').attr('src', "img/lte2hd/" + json.Player1.character.icon);
			$('#characterIMGP2').attr('src', "img/lte2hd/" + json.Player2.character.icon);
			$('#characterIMGP1').css("width", "74px");
			$('#characterIMGP1').css("height", "74px");
			$('#characterIMGP2').css("width", "74px");
			$('#characterIMGP2').css("height", "74px");
			$('#characterIMGP1').css("left", "-6px");
			$('#characterIMGP2').css("right", "-6px");
		}
		else if(json.iconStyle == "PMSTOCK")
		{
			$('#characterIMGP1').attr('src', "img/pmstock/" + json.Player1.character.icon);
			$('#characterIMGP2').attr('src', "img/pmstock/" + json.Player2.character.icon);
			$('#characterIMGP1').css("width", "74px");
			$('#characterIMGP1').css("height", "74px");
			$('#characterIMGP2').css("width", "74px");
			$('#characterIMGP2').css("height", "74px");
			$('#characterIMGP1').css("left", "-5px");
			$('#characterIMGP2').css("right", "-8px");
		}
		else
		{
			$('#characterIMGP1').css("left", "-9px");
			$('#characterIMGP2').css("right", "-9px");
			$('#characterIMGP1').attr('src', "img/no/" + json.Player1.character.icon);
			$('#characterIMGP2').attr('src', "img/no/" + json.Player2.character.icon);
		}
		//flags
		$('#flagIMGP1').attr('src', "img/flags/" + json.Player1.flag.icon);
		$('#flagIMGP2').attr('src', "img/flags/" + json.Player2.flag.icon);
		//hide unnecesary stuff
		if(json.Player1.sponsor.name == 'None')
		{
			$('#sponsorImgP1').hide();
		}
		else
		{
			$('#sponsorImgP1').show();
			$("#sponsorImgP1").attr('src', 'img/sponsors/' + json.Player1.sponsor.icon);
		}
		if(json.Player2.sponsor.name == 'None')
		{
			$('#sponsorImgP2').hide();
		}
		else
		{
			$('#sponsorImgP2').show();
			$("#sponsorImgP2").attr('src', 'img/sponsors/' + json.Player2.sponsor.icon);
		}
	}, 'json');
	//poll stuff periodically
	setTimeout("updateInformation()", pollingSpeed);
}
var nextFade = "caster";
var nextFlagFade = "country";
function fades()
{
	if(nextFade == "caster")
	{
		nextFade = "streamer"
		$('#microphone').fadeOut(fadeAnimationSpeed, function() { fadeEnd();	/* makes sure one has been faded first */ });
		setTimeout("fades()", informationBarSpeed);
	}
	else
	{
		nextFade = "caster"
		$('#streamer').fadeOut(fadeAnimationSpeed, function() { fadeEnd(); /* makes sure one has been faded first */ });
		setTimeout("fades()", informationBarSpeed);
	}
}
function flagFades()
{
	if(nextFlagFade == "country")
	{
		nextFlagFade = "character"
		$('#characterIMGP1').fadeOut(fadeAnimationSpeed);
		$('#flagIMGP1').fadeIn(fadeAnimationSpeed);
		$('#characterIMGP2').fadeOut(fadeAnimationSpeed);
		$('#flagIMGP2').fadeIn(fadeAnimationSpeed);
		setTimeout("flagFades()", fadeSpeed);
	}
	else
	{
		nextFlagFade = "country"
		$('#flagIMGP1').fadeOut(fadeAnimationSpeed);
		$('#characterIMGP1').fadeIn(fadeAnimationSpeed);
		$('#flagIMGP2').fadeOut(fadeAnimationSpeed);
		$('#characterIMGP2').fadeIn(fadeAnimationSpeed);
		
		setTimeout("flagFades()", fadeSpeed);
	}
}
function fadeEnd()
{
	if(nextFade == "caster")
	{
		$('#microphone').fadeIn(fadeAnimationSpeed);
	}
	else
	{
		$('#streamer').fadeIn(fadeAnimationSpeed);
	}
}
$(document).ready(function() {
	updateInformation();
	$('#microphone').fadeOut(0);
	$('#flagIMGP1').fadeOut(0);
	$('#flagIMGP2').fadeOut(0);
	fades();
	setTimeout("flagFades()", fadeSpeed);
	$('#ui').fadeIn(500);
});