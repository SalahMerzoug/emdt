<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="2.0"	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" encoding="utf-8"/>

  <!-- *************************** Paramètres - Valeurs par défaut *************************** -->
  
  <xsl:param name="numSemaine">37</xsl:param>
  <xsl:param name="nom_recherche_1">e</xsl:param>
  <xsl:param name="id_enseignant_2">kdrouet</xsl:param>
  <xsl:param name="id_matière_3">anglais</xsl:param>
  <xsl:param name="id_matière_4">anglais</xsl:param>
  <xsl:param name="id_enseignant_5">kdrouet</xsl:param>
  <xsl:param name="id_salle_6">langevin</xsl:param>
  <xsl:param name="id_jour_6">mardi</xsl:param>
  <xsl:param name="id_enseignant_7">kdrouet</xsl:param>
  <xsl:param name="id_jour_7">mardi</xsl:param>
  
  <!-- *************************** call-template *************************** -->

  <xsl:template match="/">
    <html>
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

      <body bgcolor="#C0C0C0"><font color="gold"><h2>
        <center>Partie II</center>
        <center>Requêtes XPath sur un emploi du temps</center>
        <center>(semaine : <xsl:value-of select="$numSemaine"/> )</center>
      </h2></font></body>
      <body bgcolor="#DDDDDD">
        
      <xsl:call-template name="Requete1">
        <xsl:with-param name="nom_recherche" select="$nom_recherche_1"/>
      </xsl:call-template>
      
      <xsl:call-template name="Requete2">
        <xsl:with-param name="id_enseignant" select="$id_enseignant_2"/>
      </xsl:call-template>

      <xsl:call-template name="Requete3">
        <xsl:with-param name="id_matière" select="$id_matière_3"/>
      </xsl:call-template>

      <xsl:call-template name="Requete4">
        <xsl:with-param name="numSemaine" select="$numSemaine"/>
        <xsl:with-param name="id_matière" select="$id_matière_4"/>
      </xsl:call-template>

      <xsl:call-template name="Requete5">
        <xsl:with-param name="numSemaine" select="$numSemaine"/>
        <xsl:with-param name="id_enseignant" select="$id_enseignant_5"/>
      </xsl:call-template>

      <xsl:call-template name="Requete6">
        <xsl:with-param name="numSemaine" select="$numSemaine"/>
        <xsl:with-param name="id_salle" select="$id_salle_6"/>
        <xsl:with-param name="id_jour" select="$id_jour_6"/>
      </xsl:call-template>

      <xsl:call-template name="Requete7">
        <xsl:with-param name="numSemaine" select="$numSemaine"/>
        <xsl:with-param name="id_enseignant" select="$id_enseignant_7"/>
        <xsl:with-param name="id_jour" select="$id_jour_7"/>
      </xsl:call-template>

      </body>
     </html>
  </xsl:template>

  <!-- *************************** template *************************** -->

<!-- *************************** Requete1 *************************** -->
  
  <xsl:template name="Requete1" >
      <xsl:param name="nom_recherche"/>
    
    <font color="blue"><h3> 
      Requête 1 : Enseignants dont le nom contient "<xsl:value-of select='$nom_recherche'/>" ? 
    </h3> </font>
    
      <xsl:for-each select='//enseignant[contains(@id,$nom_recherche)]'>
        <b> <xsl:value-of select="position()"/>) </b> <xsl:value-of select="."/><BR/><BR/>
        </xsl:for-each>
    </xsl:template>
  
  <!-- *************************** Requete2 *************************** -->

  <xsl:template name="Requete2">
    <xsl:param name="id_enseignant"/>
    
    <font color="blue"><h3>
        Requête 2 : Matières enseignées par "<xsl:value-of select='$id_enseignant'/>" ?
    </h3></font>
    
    <xsl:for-each select='//matiere[enseignant/@ref=$id_enseignant]'>
      <b>
        <xsl:value-of select="position()"/>)
      </b>
      <xsl:value-of select="."/>
      <BR></BR>
    </xsl:for-each>
  </xsl:template>

  <!-- *************************** Requete3 *************************** -->
  
    <xsl:template name="Requete3">
      <xsl:param name="id_matière"/>
      
      <font color="blue"><h3>
          Requête 3 : Professeurs enseignant : "<xsl:value-of select='$id_matière'/>" ?
      </h3></font>
      
      <!-- id professeur enseignant id_matière : //matiere[@id=$id_matière]//@ref -->
      <xsl:for-each select='//enseignant[ @id = //matiere[@id=$id_matière]//@ref ]'>
        <b>
          <xsl:value-of select="position()"/>)
        </b>
        <xsl:value-of select="."/>
        <BR></BR>
      </xsl:for-each>
    </xsl:template>

    <!-- *************************** Requete4 *************************** -->

    <xsl:template name="Requete4">
      <xsl:param name="numSemaine"/>
      <xsl:param name="id_matière"/>
      
      <font color="blue"><h3>
          Requête 4 : Enseignements de la matière avec l'identifiant: "<xsl:value-of select='$id_matière'/>" ?
      </h3></font>
      
      <xsl:for-each select='//semaine[numero=$numSemaine]//enseignement[matiere/@ref=$id_matière]'>
        <b> <xsl:value-of select="position()"/>) </b><BR/>
        <!-- on affiche l'enseignant par défaut de la matière ci celui-ci n'est pas définit -->
        <xsl:if test='not(enseignant/@ref)'>
          <xsl:value-of select="//matiere[@id=$id_matière]/enseignant/@ref"/>
        </xsl:if>
        <xsl:value-of select="enseignant/@ref "/><BR/>
        <xsl:value-of select="../../nom"/> <BR/>
        <!-- on affiche tous les horaires associés à l'enseignement -->
        <xsl:for-each select='horaire/@ref'>
          <xsl:value-of select="."/> <BR/>
        </xsl:for-each> <BR/>
      </xsl:for-each>
  </xsl:template>

  <!-- *************************** Requete5 *************************** -->

  <xsl:template name="Requete5">
    <xsl:param name="numSemaine"/>
    <xsl:param name="id_enseignant"/>
     <xsl:variable name="idDéfautProf" select="//matiere[enseignant/@ref=$id_enseignant]/@id"/>
    
    <font color="blue"><h3>
        Requête 5 : Enseignements de l'enseignant "<xsl:value-of select='$id_enseignant'/>" ?
    </h3></font>

    <!-- on recherche les enseignements dont l'id_enseignant correspond OU les enseignements sans id_enseignant définit mais dont la matière a pour enseignant principal id_enseignant (prof par défaut) -->
    <xsl:for-each select="//semaine[numero=$numSemaine]//enseignement[enseignant/@ref=$id_enseignant or ( not(enseignant)  and ( matiere/@ref= $idDéfautProf ) ) ]">
      <b><xsl:value-of select="position()"/>)</b><BR/>
      <xsl:value-of select="matiere/@ref "/> <BR/>
      <xsl:value-of select="../../nom"/> <BR/>
      <xsl:for-each select='horaire/@ref'>
        <xsl:value-of select="."/><BR/>
      </xsl:for-each> <BR/>
    </xsl:for-each>
  </xsl:template>
  
  <!-- *************************** Requete6 *************************** -->

  <xsl:template name="Requete6">
    <xsl:param name="numSemaine"/>
    <xsl:param name="id_salle"/>
    <xsl:param name="id_jour"/>
    
    <font color="blue"> <h3>
        Requête 6 : Disponibilités de la salle "<xsl:value-of select='$id_salle'/>" le "<xsl:value-of select='$id_jour'/>"  ?
    </h3> </font>

    <!-- Pour chaque tranche horaire ... -->
    <xsl:for-each select='//horaires/horaire/@id'>
      <xsl:variable name="horraireDispoOuPas" select="."/>
      <!-- horaire = horaire indisponible ? ... -->
      <xsl:choose>
        <xsl:when test="not($horraireDispoOuPas = ( //semaine[numero=$numSemaine]//enseignement[../../nom=$id_jour and salle/@ref=$id_salle]/horaire/@ref ) )">
          <font color="green"><b>
              <xsl:value-of select="position()"/>)
              Disponible à : <xsl:value-of select="//horaires/horaire[@id=$horraireDispoOuPas]"/>
        </b></font> ( <xsl:value-of select="$horraireDispoOuPas"/> )<BR/>
        </xsl:when>
        <xsl:otherwise>
          <font color="red"><b>
              <xsl:value-of select="position()"/>)
              Indisponible à : <xsl:value-of select="//horaires/horaire[@id=$horraireDispoOuPas]"/>
        </b></font> ( <xsl:value-of select="$horraireDispoOuPas"/> )<BR/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:for-each>
    
    
  </xsl:template>

  <!-- *************************** Requete7 *************************** -->

  <xsl:template name="Requete7">
    <xsl:param name="numSemaine"/>
    <xsl:param name="id_enseignant"/>
    <xsl:param name="id_jour"/>
    <xsl:variable name="idDéfautProf" select="//matiere[enseignant/@ref=$id_enseignant]/@id"/>
    
    <font color="blue"><h3>
        Requête 7 : Disponibilités de l'enseignant "<xsl:value-of select='$id_enseignant'/>" le "<xsl:value-of select='$id_jour'/>"  ?
    </h3></font>
    
    <!-- Pour chaque tranche horaire ... -->
    <xsl:for-each select='//horaires/horaire/@id'>
      <xsl:variable name="horraireDispoOuPas" select="."/>
      <!-- horaire = horaire indisponible ? ... -->
      <xsl:choose>
        <xsl:when test="not($horraireDispoOuPas = ( //semaine[numero=$numSemaine]//enseignement[../../nom=$id_jour and (enseignant/@ref=$id_enseignant) or ( not(enseignant) and ( matiere/@ref= $idDéfautProf ) ) ]/horaire/@ref ) )">
          <font color="green"><b>
              <xsl:value-of select="position()"/>)
              Disponible à : <xsl:value-of select="//horaires/horaire[@id=$horraireDispoOuPas]"/>
        </b></font> ( <xsl:value-of select="$horraireDispoOuPas"/> )<BR/>
        </xsl:when>
        <xsl:otherwise>
          <font color="red"><b>
              <xsl:value-of select="position()"/>)
              Indisponible : <xsl:value-of select="//horaires/horaire[@id=$horraireDispoOuPas]"/>
          </b></font> ( <xsl:value-of select="$horraireDispoOuPas"/> )<BR/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:for-each>

  </xsl:template>


  <!-- *************************** méthodes par défaut *************************** -->

	<xsl:template match='text()|attribute::*'>
		<!--<xsl:value-of select='.'/>-->
	</xsl:template>
  
</xsl:stylesheet>
