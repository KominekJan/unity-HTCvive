Testování úplně základních dovedností s Unity3D. A základní <b>oeApp</b> s převahou C-sharp<br />
<i>snaha byla, aby to nebylo vázáno na konkrétní platformu - dokonce se zde snažíme oprostit se od virtuální reality a začít tvořit skripty použitelné i na Androidu - snadno pak přenostitelné a HTCvive nebo jiný headset VR.</i> <br />
<br />
<b>oeZERO1601.unitypackage (3.34 MB)</b> https://www.dropbox.com/sh/axpr8yib4yh2cug/AACxXRnmrPekDEc1E7-sxFPJa?dl=0<br />
Tento jednoduchý "asset" (dočasně k dispozici ke stažení s úplnými zdroji 2016) - kompletní scéna, na které je jediná krychle a na ní text, logo, obrázek + světýlka LED. Do scény se naimportuje:<br />
<code>Import package / Custom package</code>

<hr />
zde publikované skripty v oeScripts:
- <b>oeCubes.cs</b> - generuje matici n x n černých (nebo občas šedých) krychlí, jakési "herní pole" - dynamický prostor...<br />
v Inspector se rovnou dá nastavit: (proměnné ve skripru jsou public)<br />
NumCube = počet kostek / jedna strana (při 20 vytvoří pole 20x20)<br />
OnlyBlack = (bool) buď jen černé, nebo mix s bílou a šedou - podle NumRandom<br />
<i>oeSpace?/oeRoom?</i><br />
<br />
- <b>oeInfo.cs</b> - testování jednoduchých skriptů pro získávání informací mimo Unity<br />
použití<br />
<code>oeInfo myInfoTest = new oeInfo();  //instance</code><br />
<code>int numT = myInfoTest.GetNum(2); //vrati dvoj nasobek </code><br />
<code>Debug.Log("oeInfo:GetNum():" + numT.ToString()); </code><br />
<br />
- <b>oeObjClass</b> obecná třída (mimo gameobject - ale ve jmenném prostroru, takže ostatní skript "vidí")<br />
použití<br />
<code>oeObjClass oeZero = new oeObjClass();</code><br />
<code>oeZero.SetName("testZero");</code><br />
<code>Debug.Log("--- oeObjClass: " + oeZero.GetName());</code><br />

<br />
na krychli jsou navázány skripty:<br />
- <b>oeZeroKBD.cs</b> umožní posouvat kostku po scéně (šipky) nebo rotace (ASDW) <br />
<br />
- <b>oeZeroDATA.cs</b>každých 100 frame se aktualizuje text, který ukazuje pozici a natočení kostky, zkušebně také problikává první LED světlo.. z bílé na červeno. (Randomize)	<br />
<br />
<hr />
<br />

<img src="https://raw.githubusercontent.com/octopusengine/unity-HTCvive/master/oeZERO16/images/zero16a.jpg" alt="zero16a.jpg" width="700">

- <b>oeServer 2016</b><br />
v testovací alfa verzi - pokus změny x,y,z (např. z mobilu)<br />
pozice / souřadnice - se ukládají do souboru *.json<br />
<br />
<hr />





