<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Allotments3._Default" ViewStateMode="Enabled" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="FilterContent" runat="server">
    <div class="container p-3">
        <p ID="Test" runat="server"></p>
    </div>
    <div class="container">
        <div class="row">
            <div class="col">
                <label for="companyDropDown">Company</label>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server" ViewStateMode="Enabled">
                    <ContentTemplate>
                        <asp:DropDownList ID="companyDropDown" class="form-control" runat="server" OnSelectedIndexChanged="companyDropDown_SelectedIndexChanged" AutoPostBack="true" ViewStateMode="Enabled">
                            <asp:ListItem Text="text1" />
                            <asp:ListItem Text="text2" />
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div class="col">
                <label for="yearDropDown">Year</label>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="yearDropDown" class="form-control" runat="server" OnSelectedIndexChanged="yearDropDown_SelectedIndexChanged" CausesValidation="false" AutoPostBack="true">
                            <asp:ListItem Text="text1" />
                            <asp:ListItem Text="text2" />
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div class="col">
                <label for="monthDropDown">Month</label>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="monthDropDown" class="form-control" runat="server" OnSelectedIndexChanged="monthDropDown_SelectedIndexChanged" CausesValidation="false" AutoPostBack="true">
                            <asp:ListItem Text="text1" />
                            <asp:ListItem Text="text2" />
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ResultsUpdatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
        </ContentTemplate> 
    </asp:UpdatePanel>
</asp:Content>
