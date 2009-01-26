<?xml version="1.0" encoding="ISO-8859-15"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="2.0">

	<!-- Donne l'abscisse correspondant à l'heure donnée. 
	     Fonction interne utile pour les fonctions x1 et x2 uniquement. -->
	<xsl:template name="x">
		<!-- Heure donnée de manière littérale (e.g. '8h30') -->
		<xsl:param name="heure"/>
		<xsl:variable name="h" select="number(substring-before($heure,'h'))"/>
		<xsl:variable name="m" select="number(substring-after($heure,'h'))"/>
		<xsl:variable name="minutes" select="((($h)-8)*60)+($m)"/>
		<xsl:variable name="retrait">
			<xsl:choose>
				<xsl:when test="$h &lt; 13">0</xsl:when>
				<xsl:otherwise>60</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="20+(($minutes)-($retrait))*2.5"/>
	</xsl:template>

	<!-- Donne l'abscisse du point haut/gauche de la vignette d'un enseignement.
	     Cette fonction devra utiliser la fonction x.  -->
	<xsl:template name="x1">
		<!-- Identifiant de l'horaire de l'enseignement -->
		<xsl:param name="horaire"/>
		<xsl:variable name="horaires" select="/emploi-du-temps/horaires/horaire"/>
		<xsl:call-template name="x">
			<xsl:with-param name="heure" select="$horaires[@id=$horaire]/debut"/>
		</xsl:call-template>
	</xsl:template>

	<!-- Donne l'abscisse du point haut/droite de la vignette d'un enseignement.
       Cette fonction devra utiliser la fonction x.  -->
	<xsl:template name="x2">
		<!-- Identifiant de l'horaire de l'enseignement -->
		<xsl:param name="horaire"/>
		<xsl:variable name="horaires" select="/emploi-du-temps/horaires/horaire"/>
		<xsl:call-template name="x">
			<xsl:with-param name="heure" select="$horaires[@id=$horaire]/fin"/>
		</xsl:call-template>
	</xsl:template>

	<!-- Donne l'ordonnée du point haut/gauche de la vignette d'un enseignement. -->
	<xsl:template name="y">
		<!-- Jour de l'enseignement (e.g. 'lundi') -->
		<xsl:param name="jour"/>
		<!-- Groupe ('1' ou '2') de l'enseignement. Ce paramètre est optionel. -->
		<xsl:param name="groupe"/>
		<xsl:variable name="j">
			<xsl:choose>
				<xsl:when test="$jour='lundi'">0</xsl:when>
				<xsl:when test="$jour='mardi'">1</xsl:when>
				<xsl:when test="$jour='mercredi'">2</xsl:when>
				<xsl:when test="$jour='jeudi'">3</xsl:when>
				<xsl:when test="$jour='vendredi'">4</xsl:when>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="g">
			<xsl:choose>
				<xsl:when test="$groupe='2'">2</xsl:when>
				<xsl:otherwise>1</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="50+(155*($j))+(75*(($g)-1))"/>
	</xsl:template>

	<!-- Donne la hauteur de la vignette d'un enseignement. -->
	<xsl:template name="hauteur">
		<!-- Groupe ('1' ou '2') de l'enseignement. Ce paramètre est optionel. -->
		<xsl:param name="groupe"/>
		<xsl:variable name="g">
			<xsl:choose>
				<xsl:when test="$groupe">1</xsl:when>
				<xsl:otherwise>2</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:value-of select="70+75*(($g)-1)"/>
	</xsl:template>

</xsl:stylesheet>