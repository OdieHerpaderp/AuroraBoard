var pollingSpeed = 250;//in ms
var fadeSpeed = 3500; // in ms
var fadeAnimationSpeed = 500;
var informationBarSpeed = 6000;
document.documentElement.style.overflow = 'hidden';  // firefox, chrome

function updateInformation()
{
	$.get('http://127.0.0.1:8081/getCrewValues', function (json) {
		//player names
		$('#crew1Text').text(json.Crew1);
		$('#crew2Text').text(json.Crew2);
		// play with font widths
		if($('#crew1Text').text().length > 20) {
			$('#crew1Text').css("font-stretch", "condensed");
		}
		else if($('#crew1Text').text().length < 14) {
			$('#crew1Text').css("font-stretch", "ultra-expanded");
		}
		else {
			$('#crew1Text').css("font-stretch", "normal");
		}
		
		if($('#crew2Text').text().length > 20) {
			$('#crew2Text').css("font-stretch", "condensed");
		}
		else if($('#crew2Text').text().length < 14) {
			$('#crew2Text').css("font-stretch", "ultra-expanded");
		}
		else {
			$('#crew2Text').css("font-stretch", "normal");
		}
		//scores
		$('#crew1Score').text(json.Crew1Stocks.toString());
		$('#crew2Score').text(json.Crew2Stocks.toString());
		//topbar
		//$("#matchType").text(json.matchType);
		$("#round").text(json.round);
		$("#tournamentName").text(json.tournamentName);
		//information bar
		$("#casterText").text(json.caster);
		$("#streamerText").text(json.streamer);
		//character images
		var stocksA = json.Crew1Stocks;
		var stocksB = json.Crew2Stocks;

		$('#crewA1Text').text(json.A1.name);
		$('#crewCharA1').attr('src', "img/lte2hd/" + json.A1.character.icon);
		if (stocksA < 13)
		{ $('#crewA2Text').text(json.A2.name); $('#crewCharA2').attr('src', "img/lte2hd/" + json.A2.character.icon); }
		else
		{ $('#crewA2Text').text("? ? ? ? ? ? ? ?"); $('#crewCharA2').attr('src', "img/lte2hd/random.png");}
		if (stocksA < 9)
		{ $('#crewA3Text').text(json.A3.name); $('#crewCharA3').attr('src', "img/lte2hd/" + json.A3.character.icon);}
		else
		{ $('#crewA3Text').text("? ? ? ? ? ? ? ?");  $('#crewCharA3').attr('src', "img/lte2hd/random.png");}
		if (stocksA < 5)
		{ $('#crewA4Text').text(json.A4.name); $('#crewCharA4').attr('src', "img/lte2hd/" + json.A4.character.icon);}
		else
		{ $('#crewA4Text').text("? ? ? ? ? ? ? ?");  $('#crewCharA4').attr('src', "img/lte2hd/random.png");}

		$('#crewB1Text').text(json.B1.name);
		$('#crewCharB1').attr('src', "img/lte2hd/" + json.B1.character.icon);
		if (stocksB < 13)
		{ $('#crewB2Text').text(json.B2.name); $('#crewCharB2').attr('src', "img/lte2hd/" + json.B2.character.icon); }
		else
		{ $('#crewB2Text').text("? ? ? ? ? ? ? ?"); $('#crewCharB2').attr('src', "img/lte2hd/random.png");}
		if (stocksB < 9)
		{ $('#crewB3Text').text(json.B3.name); $('#crewCharB3').attr('src', "img/lte2hd/" + json.B3.character.icon);}
		else
		{ $('#crewB3Text').text("? ? ? ? ? ? ? ?");  $('#crewCharB3').attr('src', "img/lte2hd/random.png");}
		if (stocksB < 5)
		{ $('#crewB4Text').text(json.B4.name); $('#crewCharB4').attr('src', "img/lte2hd/" + json.B4.character.icon);}
		else
		{ $('#crewB4Text').text("? ? ? ? ? ? ? ?");  $('#crewCharB4').attr('src', "img/lte2hd/random.png");}


		if (stocksA > 12)
		{
			$('#stockIMGA1').attr('src', "img/stock" + (stocksA - 12) + ".png");
			$('#stockIMGA2').attr('src', "img/stock" + (4) + ".png");
			$('#stockIMGA3').attr('src', "img/stock" + (4) + ".png");
			$('#stockIMGA4').attr('src', "img/stock" + (4) + ".png");
			$('#characterIMGP1').attr('src', "img/characters/" + json.A1.character.icon);
			$('#crew1curText').text(json.A1.name);
		}
		else if (stocksA > 8)
		{
			$('#stockIMGA1').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGA2').attr('src', "img/stock" + (stocksA - 8) + ".png");
			$('#stockIMGA3').attr('src', "img/stock" + (4) + ".png");
			$('#stockIMGA4').attr('src', "img/stock" + (4) + ".png");
			$('#characterIMGP1').attr('src', "img/characters/" + json.A2.character.icon);
			$('#crew1curText').text(json.A2.name);
		}
		else if (stocksA > 4)
		{
			$('#stockIMGA1').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGA2').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGA3').attr('src', "img/stock" + (stocksA - 4) + ".png");
			$('#stockIMGA4').attr('src', "img/stock" + (4) + ".png");
			$('#characterIMGP1').attr('src', "img/characters/" + json.A3.character.icon);
			$('#crew1curText').text(json.A3.name);
		}
		else
		{
			$('#stockIMGA1').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGA2').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGA3').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGA4').attr('src', "img/stock" + (stocksA) + ".png");
			$('#characterIMGP1').attr('src', "img/characters/" + json.A4.character.icon);
			$('#crew1curText').text(json.A4.name);
		}

		if (stocksB > 12)
		{
			$('#stockIMGB1').attr('src', "img/stock" + (stocksB - 12) + ".png");
			$('#stockIMGB2').attr('src', "img/stock" + (4) + ".png");
			$('#stockIMGB3').attr('src', "img/stock" + (4) + ".png");
			$('#stockIMGB4').attr('src', "img/stock" + (4) + ".png");
			$('#characterIMGP2').attr('src', "img/characters/" + json.B1.character.icon);
			$('#crew2curText').text(json.B1.name);
		}
		else if (stocksB > 8)
		{
			$('#stockIMGB1').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGB2').attr('src', "img/stock" + (stocksB - 8) + ".png");
			$('#stockIMGB3').attr('src', "img/stock" + (4) + ".png");
			$('#stockIMGB4').attr('src', "img/stock" + (4) + ".png");
			$('#characterIMGP2').attr('src', "img/characters/" + json.B2.character.icon);
			$('#crew2curText').text(json.B2.name);
		}
		else if (stocksB > 4)
		{
			$('#stockIMGB1').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGB2').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGB3').attr('src', "img/stock" + (stocksB - 4) + ".png");
			$('#stockIMGB4').attr('src', "img/stock" + (4) + ".png");
			$('#characterIMGP2').attr('src', "img/characters/" + json.B3.character.icon);
			$('#crew2curText').text(json.B3.name);
		}
		else
		{
			$('#stockIMGB1').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGB2').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGB3').attr('src', "img/stock" + (0) + ".png");
			$('#stockIMGB4').attr('src', "img/stock" + (stocksB) + ".png");
			$('#characterIMGP2').attr('src', "img/characters/" + json.B4.character.icon);
			$('#crew2curText').text(json.B4.name);
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

	}
	else
	{

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