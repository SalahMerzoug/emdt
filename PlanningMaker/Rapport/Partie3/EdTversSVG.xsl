<?xml version="1.0" encoding="ISO-8859-15"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xlink="http://www.w3.org/1999/xlink"
                version="2.0">
	<xsl:output method="html" encoding="UTF-8" indent="yes"/>
	<xsl:include href="Fonctions.xsl"/>
	<xsl:include href="ModelesSVG.xsl"/>
	
	<xsl:param name="numeroSemaine">37</xsl:param>
	
	<xsl:template match="/">
		<html xmlns:svg="http://www.w3.org/2000/svg">
			<object id="AdobeSVG" CLASSID="clsid:78156a80-c6a1-4bbf-8e6a-3cd390eeb4e2">
			</object>
			<xsl:processing-instruction name="import">namespace="svg" implementation="#AdobeSVG"</xsl:processing-instruction>
			<xsl:apply-templates/>
		</html>
	</xsl:template>
	
	<!-- Votre code XSLT ici -->
	<xsl:template match="emploi-du-temps">
		<svg:svg xmlns:svg="http://www.w3.org/2000/svg" width="1300" height="850" font-family="Times New Roman">
			<xsl:apply-templates/>
		</svg:svg>
	</xsl:template>
	
	<xsl:template match="horaires/horaire">
		<xsl:call-template name="trancheHoraire">
			<xsl:with-param name="x1">
				<xsl:call-template name="x1">
					<xsl:with-param name="horaire" select="@id"/>
				</xsl:call-template>
			</xsl:with-param>
			<xsl:with-param name="x2">
				<xsl:call-template name="x2">
					<xsl:with-param name="horaire" select="@id"/>
				</xsl:call-template>
			</xsl:with-param>
			<xsl:with-param name="heure1" select="debut"/>
			<xsl:with-param name="heure2" select="fin"/>
		</xsl:call-template>
	</xsl:template>
	
	<xsl:template match="semaines/semaine">
		<xsl:if test="numero=$numeroSemaine">
			<xsl:call-template name="fond">
				<xsl:with-param name="semaine" select="numero"/>
				<xsl:with-param name="date" select="date"/>
				<xsl:with-param name="annee" select="/emploi-du-temps/annee"/>
				<xsl:with-param name="division" select="/emploi-du-temps/division"/>
				<xsl:with-param name="promotion" select="/emploi-du-temps/promotion"/>
			</xsl:call-template>
			<xsl:apply-templates/>
		</xsl:if>
	</xsl:template>
	
	<xsl:template match="enseignements/enseignement">
		<xsl:variable name="id-matiere" select="matiere/@ref"/>
		<xsl:variable name="matieres" select="/emploi-du-temps/matieres/matiere"/>
		<xsl:variable name="matiere" select="$matieres[@id=$id-matiere]"/>
		<xsl:variable name="id-enseignant">
			<xsl:choose>
				<xsl:when test="enseignant">
					<xsl:value-of select="enseignant/@ref"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="$matiere/enseignant/@ref[1]"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:variable>
		<xsl:variable name="enseignants" select="/emploi-du-temps/enseignants/enseignant"/>
		<xsl:variable name="enseignant" select="$enseignants[@id=$id-enseignant]"/>
		<xsl:variable name="prenom" select="$enseignant/prenom"/>
		<xsl:variable name="nom" select="$enseignant/nom"/>
		<xsl:variable name="id-salle" select="salle/@ref"/>
		<xsl:variable name="salles" select="/emploi-du-temps/salles/salle"/>
		<xsl:variable name="salle" select="$salles[@id=$id-salle]"/>
		<xsl:call-template name="vignette">
			<xsl:with-param name="x1">
				<xsl:call-template name="x1">
					<xsl:with-param name="horaire" select="horaire[1]/@ref"/>
				</xsl:call-template>
			</xsl:with-param>
			<xsl:with-param name="x2">
				<xsl:choose>
					<xsl:when test="count(horaire)=1">
						<xsl:call-template name="x2">
							<xsl:with-param name="horaire" select="horaire[1]/@ref"/>
						</xsl:call-template>
					</xsl:when>
					<xsl:when test="count(horaire)=2">
						<xsl:call-template name="x2">
							<xsl:with-param name="horaire" select="horaire[2]/@ref"/>
						</xsl:call-template>
					</xsl:when>
				</xsl:choose>
			</xsl:with-param>
			<xsl:with-param name="y">
				<xsl:call-template name="y">
					<xsl:with-param name="jour" select="../../nom"/>
					<xsl:with-param name="groupe" select="numeroGroupe"/>
				</xsl:call-template>
			</xsl:with-param>
			<xsl:with-param name="hauteur">
				<xsl:call-template name="hauteur">
					<xsl:with-param name="groupe" select="numeroGroupe"/>
				</xsl:call-template>
			</xsl:with-param>
			<xsl:with-param name="type" select="type"/>
			<xsl:with-param name="matiere" select="$matiere/titre"/>
			<xsl:with-param name="enseignant" select="concat(substring($prenom,1,1),'. ', $nom)"/>
			<xsl:with-param name="salle" select="$salle/nom"/>
			<xsl:with-param name="groupe" select="numeroGroupe"/>
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="text()"/>
	<!-- Fin code XSLT -->
</xsl:stylesheet>