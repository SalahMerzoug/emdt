<?xml version="1.0" encoding="ISO-8859-15"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns:xlink="http://www.w3.org/1999/xlink"
				xmlns:svg="http://www.w3.org/2000/svg"
				version="2.0">

	<xsl:template name="fond">
		<xsl:param name="semaine"/>
		<xsl:param name="date"/>
		<xsl:param name="annee"/>
		<xsl:param name="division"/>
		<xsl:param name="promotion"/>
		<!-- titre -->
		<svg:text x="0" y="10" font-size="16" font-weight="bold">
			Semaine <xsl:text> </xsl:text>
			<xsl:value-of select="$semaine"/> <xsl:text> </xsl:text>
			(<xsl:value-of select="$date"/>)
		</svg:text>
		<svg:text x="1290" y="10" font-size="16" font-weight="bold" text-anchor="end">
			Division
			<xsl:value-of select="$annee"/>
			<xsl:value-of select="$division"/> -
			Promotion
			<xsl:value-of select="$promotion"/>
		</svg:text>
		<!-- liste jours -->
		<svg:g font-size="16" font-weight="bold" fill="white">
			<svg:g transform="rotate(-90)translate(10,0)">
				<svg:g transform="translate(-205,0)">
					<svg:rect width="145" height="20" fill="gray" opacity="0.8"/>
					<svg:text text-anchor="middle" x="72" y="15">Lundi</svg:text>
				</svg:g>
				<svg:g transform="translate(-360,0)">
					<svg:rect width="145" height="20" fill="gray" opacity="0.8"/>
					<svg:text text-anchor="middle" x="72" y="15">Mardi</svg:text>
				</svg:g>
				<svg:g transform="translate(-515,0)">
					<svg:rect width="145" height="20" fill="gray" opacity="0.8"/>
					<svg:text text-anchor="middle" x="72" y="15">Mercredi</svg:text>
				</svg:g>
				<svg:g transform="translate(-670,0)">
					<svg:rect width="145" height="20" fill="gray" opacity="0.8"/>
					<svg:text text-anchor="middle" x="72" y="15">Jeudi</svg:text>
				</svg:g>
				<svg:g transform="translate(-825,0)">
					<svg:rect width="145" height="20" fill="gray" opacity="0.8"/>
					<svg:text text-anchor="middle" x="72" y="15">Vendredi</svg:text>
				</svg:g>
			</svg:g>
		</svg:g>
	</xsl:template>

	<xsl:template name="trancheHoraire">
		<xsl:param name="x1"/>
		<xsl:param name="x2"/>
		<xsl:param name="heure1"/>
		<xsl:param name="heure2"/>
		<svg:g transform="translate(0,40)">
			<svg:rect x="{$x1}" y="0" height="785" width="{($x2)-($x1)}" stroke="none" fill="none"/>
			<svg:line x1="{$x1}" y1="0" x2="{$x1}" y2="785" stroke="black" opacity="0.2"/>
			<svg:line x1="{$x2}" y1="0" x2="{$x2}" y2="785" stroke="black" opacity="0.2"/>
			<svg:text x="{($x1)+2}" y="-4" text-anchor="start">
				<xsl:value-of select="$heure1"/>
			</svg:text>
			<svg:text x="{($x2)-2}" y="-4" text-anchor="end">
				<xsl:value-of select="$heure2"/>
			</svg:text>
		</svg:g>
	</xsl:template>

	<xsl:template name="vignette">
		<xsl:param name="x1"/>
		<xsl:param name="x2"/>
		<xsl:param name="y"/>
		<xsl:param name="hauteur"/>
		<xsl:param name="type"/>
		<xsl:param name="matiere"/>
		<xsl:param name="enseignant"/>
		<xsl:param name="salle"/>
		<xsl:param name="groupe"/>
		<!-- TODO vignette à insérer ici  -->
		<svg:g>
			<svg:rect x="{$x1}" y="{$y}" width="{($x2)-($x1)}" height="{$hauteur}" fill="white" stroke="black" stroke-width="1" />
			<xsl:choose>
				<xsl:when test="$type='COURS'">
					<svg:rect x="{$x1}" y="{$y}" width="20" height="{$hauteur}" fill="yellow"/>
				</xsl:when>
				<xsl:when test="$type='TP'">
					<svg:rect x="{$x1}" y="{$y}" width="20" height="{$hauteur}" fill="limegreen"/>
				</xsl:when>
				<xsl:when test="$type='TD'">
					<svg:rect x="{$x1}" y="{$y}" width="20" height="{$hauteur}" fill="royalblue"/>
				</xsl:when>
			</xsl:choose>
			<xsl:if test="$groupe='1' or $groupe='2'">
				<svg:g transform="translate({$x2},{($y)+($hauteur)})">
					<svg:polygon points="0,-30 0,0 -30,0" fill="dimgrey"/>
					<svg:text x="-10" y="-5" font-size="16" fill="white">
						<xsl:value-of select="$groupe"/>
					</svg:text>
				</svg:g>
			</xsl:if>
			<svg:text x="{($x1)+25}" y="{($y)+15}" font-size="16" font-weight="bold">
				<xsl:value-of select="$matiere"/>
			</svg:text>
			<svg:text x="{($x1)+25}" y="{($y)+45}" font-size="16" font-style="italic">
				<xsl:value-of select="$enseignant"/>
			</svg:text>
			<svg:text x="{($x1)+25}" y="{($y)+65}" font-size="16">
				<xsl:value-of select="$salle"/>
			</svg:text>
			<!-- problème avec firefox -->
			<svg:g transform="rotate(270, {($x1)+14}, {($y)+5})">
				<svg:text text-anchor="end" x="{($x1)+14}" y="{($y)+5}" font-weight="bold" font-size="14">
					<xsl:value-of select="$type"/>
				</svg:text>
			</svg:g>
		</svg:g>
		<!-- Fin TODO -->
	</xsl:template>
</xsl:stylesheet>