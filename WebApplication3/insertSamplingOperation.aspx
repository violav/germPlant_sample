<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="insertSamplingOperation.aspx.cs" Inherits="WebApplication3.insertSamplingOperation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="jumbotron">
        <h1>Germplants</h1>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Genetic operation history</h2>      
             <p>
                researcher <br />
                <asp:DropDownList ID="ddlResearcher" runat="server" DataValueField="researcher_id" DataTextField="familyname">
                </asp:DropDownList>
            </p>
            <p>
                operation date (when did the researcher made the operation?) <br />
              <asp:Calendar runat="server" ID="dateOp" ></asp:Calendar>
            </p>      
               <p> 
                note (do you wish to add notes?) <br />
              <asp:TextBox ID="txtnote" runat="server"></asp:TextBox>
            </p>      
               <p>
               path file (upload you material here) <br />
              <asp:FileUpload id="pathFile" runat="server"/>
            </p>      
        </div>
        <div class="col-md-6">
            <h2>Sampling Operation</h2>
              <p>
                sampling country <br />
                <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" 
                    runat="server" DataTextField="country_id" DataValueField="country_name">
                </asp:DropDownList>
            </p>
            <p>
                Sempling Site <br />
                <asp:DropDownList ID="ddlSamplingSite" runat="server" DataTextField="location" DataValueField="descr">
                </asp:DropDownList>
            </p>
             
        </div>
       
    </div>
     <div class="row">
         <asp:Button  class="btn btn-default" runat="server" ID="btnInsertGenOp" OnClick="btnInsertGenOp_Click" Text="Save Genetic Operation"/>
     </div>
</asp:Content>
