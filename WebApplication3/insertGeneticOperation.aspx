<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="insertGeneticOperation.aspx.cs" Inherits="WebApplication3.insertOperation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="jumbotron">
        <h1>Germplants</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Genetic operation history</h2>      
            <p>
                genetic program <br />
                <asp:DropDownList ID="ddlProg" runat="server" DataValueField="gen_prog_id" DataTextField="description">
                </asp:DropDownList>
            </p>
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
        <div class="col-md-4">
            <h2>Genetic Operation</h2>
            <p>
                genetic population <br />
                <asp:DropDownList ID="ddlGenPopulation" runat="server" DataTextField="description" DataValueField="operation_id">
                </asp:DropDownList>
            </p>
                 <p>
                text (do you wish to add text on genetic population?) <br />
              <asp:TextBox ID="txtGenPop" runat="server"></asp:TextBox>
            </p>   
                 <p>
                note (do you wish to add notes on genetic population?) <br />
              <asp:TextBox ID="txtGenPopNote" runat="server"></asp:TextBox>
            </p>   
              <p>
                genealogy  <br />
              <asp:TextBox ID="txtGenealogy" runat="server"></asp:TextBox>
            </p> 
               <p>
               father (same genus and species) <br />
                <asp:DropDownList ID="ddlAccessionFather" runat="server" DataTextField="description" DataValueField="operation_id">
                </asp:DropDownList>
                <asp:TextBox ID="txtFather" runat="server"></asp:TextBox>
            </p>
              <p>
               mother (same genus and species) <br />
                <asp:DropDownList ID="ddlAccessionMother" runat="server" DataTextField="description" DataValueField="operation_id">
                </asp:DropDownList>
                     <asp:TextBox ID="txtMother" runat="server"></asp:TextBox>
            </p>
             <p>
               father operation <br />
                <asp:DropDownList ID="ddlOperations" runat="server" DataTextField="description" DataValueField="operation_id">
                </asp:DropDownList>
            </p>
            <p>
                fieldnumber <br />
              <asp:TextBox ID="txtField" runat="server"></asp:TextBox>
            </p>   
            <p>
                subfieldnumber <br />
              <asp:TextBox ID="txtSubField" runat="server"></asp:TextBox>
            </p> 
            <p>
               father operation <br />
                <asp:DropDownList  ID="ddlFatherOp" runat="server" DataTextField="description" DataValueField="operation_id">
                </asp:DropDownList>

                  <asp:TextBox ID="txtFatherOP" runat="server"></asp:TextBox>
            </p>
        </div>
          <div class="col-md-4">
            <h2>Genetic Operation Detail</h2>
            <p>
                genetic operation <br />
                <asp:CheckBoxList ID="chkPossibleOp" runat="server" OnSelectedIndexChanged="lstPossibleOperations_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
            </p>
         </div>
    </div>
     <div class="row">
         <asp:Button  class="btn btn-default" runat="server" ID="btnInsertGenOp" OnClick="btnInsertGenOp_Click" Text="Save Genetic Operation"/>
     </div>
</asp:Content>
