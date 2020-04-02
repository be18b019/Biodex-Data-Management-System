function H = Biodex_GUI

close all;
clear;
clc;

ss = get(0,'ScreenSize'); % get Screensize
Data.hfm = figure('Position', ss,'Name','Main Figure', 'Color', [0 0 0]); % ...creating main figure with screensize ss
try 
    fclose(Data.s); % Wenn ein Serial Port geöffnet wurde, schließe ihn
catch a % kann ignoriert werden
end
    delete(instrfind); % lösche alle Serial Ports. Dies bezweckt, dass es zu keinen Problemen führt, wenn ein neuer Serial Port geöffnet wird

if ispc % Wenn es sich um einen Windows PC handelt, dann...
    Data.s = serial('COM7');
else % Falls es ein Apple ist...
    Data.s = serial('/dev/tty.usbmodemfa131');
end

Data.y = [];
Data.tmp = [];
Data.handlekraftplot = [];

%% Buttons und Checkboxen erstellen

Data.hpb_start = uicontrol('Style', 'pushbutton','String','Start', 'FontSize', 11,'Position',[50 20 100 50],... %...absolute position to the figures client / p(4)-30
                      'Callback', {@pb1,Data.hfm}); %...call function pb1 for start/stop the data transfer and passing parameter Data.f by pushing this button
Data.hpb_load = uicontrol('Style', 'pushbutton', 'String', 'Load', 'FontSize', 11, 'Position', [750 20 100 50],...
                          'Callback', {@pb_load, Data.hfm});
Data.hpb_stop = uicontrol('Style', 'pushbutton', 'String', 'Stop', 'FontSize', 11, 'Position', [150 20 100 50],...
                          'Callback', {@pb_stop, Data.hfm},'Enable', 'off');
Data.hpb_export = uicontrol('Style', 'pushbutton', 'String', 'Export', 'FontSize', 11, 'Position', [850 20 100 50],...
                          'Callback', {@pb_export, Data.hfm}, 'Enable', 'off');
Data.hcheckkraft = uicontrol('Style', 'checkbox', 'ForegroundColor', [1 1 1], 'String', 'Kraftverlauf', 'Enable', 'off','Position', [300 10 100 50], 'Value', 1, 'FontSize', 11, 'Callback',{@kraftverlauf, Data.hfm});
Data.hcheckkraft_kontrolle = uicontrol('Style', 'checkbox','ForegroundColor', [1 1 1], 'String', 'Kraftrampe','Enable','off', 'Position', [300 40 100 50], 'Value', 0, 'FontSize', 11, 'Callback',{@kraftkontrolle, Data.hfm});
Data.hcheckpos = uicontrol('Style', 'checkbox', 'ForegroundColor', [1 1 1],'String', 'Geschwindigkeitsverlauf', 'Position', [400 10 190 50], 'Value', 1, 'FontSize', 11, 'Callback',{@posverlauf, Data.hfm});
Data.hcheckgeschw = uicontrol('Style', 'checkbox', 'ForegroundColor', [1 1 1],'String', 'Winkelverlauf', 'Position', [580 10 130 50], 'Value', 1, 'FontSize', 11, 'Callback',{@geschwverlauf, Data.hfm});


if ispc % Falls es sich um ein Windows-Betriebssystem handelt...
   set(Data.hcheckkraft ,'BackgroundColor', [0 0 0]);
   set(Data.hcheckkraft_kontrolle ,'BackgroundColor', [0 0 0]);
   set(Data.hcheckpos ,'BackgroundColor', [0 0 0]);
   set(Data.hcheckgeschw ,'BackgroundColor', [0 0 0]);
end

Data.kraftplot = get(Data.hcheckkraft, 'Value');
Data.kraftplotkontrolle = get(Data.hcheckkraft_kontrolle, 'Value');
Data.posplot = get(Data.hcheckpos, 'Value');
Data.geschwplot = get(Data.hcheckgeschw, 'Value');
            
% Textbox erstellen
annotation('textbox',...
    [0.04 0.9 0.5 0.1],...
    'String',{'Kraftmessung - Biodex'},...
    'FontSize',32,...
    'FontName','TimesNewRoman',...
    'FontWeight','bold',...
    'BackgroundColor',[0 0 0],...
    'Color',[1 1 1]);

% Timer erstellen
Data.ht1 = timer('TimerFcn',{@t1,Data.hfm},'Period',1/30,'ExecutionMode','fixedRate'); % timer with periode of 50ms
Data.htplot = timer('TimerFcn',{@tplot,Data.hfm},'Period',1/20,'ExecutionMode','fixedRate','StartDelay',2);


Initialisierung; % Verschiedene benötigte Variablen initialisieren -> siehe .m file "Initialisierung"
set(Data.hfm,'UserData',Data); %...write initial data to main figure userdata
H = Data.hfm;
%end main function

%==========
function pb1(obj,event,h) % ...called by pushbutton 1, start the data transfer
    Data = get(h,'UserData'); %...read actual data from main figure userdata
    set(Data.hpb_start,'Enable','off');
    set(Data.hpb_export,'Enable','off'); 
    set(Data.hpb_stop,'Enable','on'); 
    Initialisierung; % Verschiedene benötigte Variablen initialisieren -> siehe .m file "Initialisierung"

%% aus RS232_GUI: es wird versucht den oben genannten COM Port zu öffnen
try fopen(Data.s); % falls der oben erwähnte COM Port nicht verfügbar ist, werden alle von COM 0 - 9 durchsucht
catch err

p = regexp(err.message, 'COM\w*', 'match'); % Sucht nach verfügbaren COM Ports in der error-message
p = p(2:end); % erstes p = falscher COM port... wird einfach rausgeschnitten
Data.ports = cell(0,1);
for i = 1:size(p,2)
    Data.s = serial(p{i});
    try fopen(Data.s);
        Data.ports(end+1) = p(i);
        fclose(Data.s);
    catch err
        continue
    end
end
end
%% ende RS232_GUI

if Data.kraftplot && Data.posplot && Data.geschwplot % Wenn alle Checkboxen aktiviert wurden...
   Data.hl = subplots_darstellen(Data.xLength, Data.y, Data.numChannels, Data.yScale, Data.indYScale); 
end

if Data.kraftplot && ~Data.posplot && ~Data.geschwplot % Falls nur die Checkbox "Kraftplot" aktiviert wurde...
    positionVector1 = [0.05, 0.25, 0.9, 0.6];
    Data.subplot1 = subplot('position', positionVector1);
    Data.hl = line(1:Data.xLength,Data.y(end-999:end,1).*1.04-552,'Color', [0, 206/255, 209/255],'Visible', 'on');
    set(gca,'color',[0 0 0], 'XColor', [1 1 1], 'YColor', [1 1 1]); 
end
 
    set(h,'UserData',Data); %...write actual data to main figure userdata
    
    start(Data.ht1); % start timer (liest Daten vom AD-Wandler ein und wandelt die String Daten in Zahlenwerte um)
    start(Data.htplot); % start timer plot (plottet die empfangenen und umgewandelten Daten)
    
%end pb_start function
%==========

function pb_load(obj,event,h) % Load Button
    Data = get(h,'UserData'); %...read actual data from main figure userdata
       
  
             [Data.names Data.path] = uigetfile();
              Data.endung = Data.names(max(strfind(Data.names , '.')):(length(Data.names)));
              if strcmp(Data.endung, '.xls')
              Data.file = xlsread([Data.path Data.names]);
              elseif strcmp(Data.endung, '.mat');
              data_hilf = load([Data.path Data.names]);
              data_name = fieldnames(data_hilf);
              Data.file = data_hilf.(cell2mat(data_name));
              end
              
               
               %% Erstelle Plots von vorhandenen Daten
               % Subplot Drehmoment
               Data.globalAxes = axes('units','normalized','position', [0.2 0.1 0.5 0.7]);
               set(Data.globalAxes,'UserData', {@loadButton, h});
               axis(Data.globalAxes);
               positionVector1 = [0.05, 0.65, 0.7, 0.2];
               Data.subplot1 = subplot('position', positionVector1);
               plot(Data.file(:,1).*1.04 - 552, 'Color', [0, 206/255, 209/255]);
               set(gca,'color',[0 0 0], 'XColor', [1 1 1],'XTick', [], 'YColor', [1 1 1]); 
               ylabel('Drehmoment [Nm]');
               
               %Subplot Winkelgeschwindigkeit
               positionVector2 = [0.05, 0.45, 0.7, 0.2];
               Data.subplot2 = subplot('position', positionVector2);
               plot(Data.file(:,2)*0.883 - 459.0, 'Color', [138/255,43/255,226/255]);
               set(gca,'color',[0 0 0], 'XColor', [1 1 1],'XTick', [], 'YColor', [1 1 1]);
               ylabel('W.-Geschw. [°/s]');
              
               %Subplot Winkel
               positionVector3 = [0.05, 0.25, 0.7, 0.2];
               Data.subplot3 = subplot('position', positionVector3);
               plot(Data.file(:,3)*0.363 - 99.3, 'Color', [0/255,191/255,255/255]);
               set(gca,'color',[0 0 0], 'XColor', [1 1 1], 'YColor', [1 1 1]);
               ylabel('Winkel [°]');
               xlabel('Frames'); 
    
       
    set(h,'UserData',Data); %...write actual data to main figure userdata
%end pb_load function
%==========

function pb_stop(obj,event,h) % Stop button
    Data = get(h,'UserData'); %...read actual data from main figure userdata
    set(Data.hpb_start,'Enable','on');   
    set(Data.hpb_stop,'Enable','off');  
    set(Data.hpb_export,'Enable','on'); 
    
    stop(Data.ht1); %Stoppe Timer1
    stop(Data.htplot); %Stoppe Timer_Plot
    fclose(Data.s); %Schliße Serial Port
    
    % Erstelle Variable für Kalibrierte Daten
    Data.kal = [Data.y(1001:end,1).*1.04 - 552, Data.y(1001:end,2).*0.883 - 459.0, Data.y(1001:end,3).*0.363 - 99.3];
    
    % Schreibe die Variablen in den Workspace
    assignin('base', 'OrigWerte', Data.y(1001:end,:));
    assignin('base', 'KalibrierteWerte', Data.kal);

    assignin('base', 'geloescht', Data.geloescht);
    assignin('base', 'ADWandler_StringDaten', Data.tmp);
   
  Data.y2 = Data.y(1001:end,:); % Wird für "Export" Button verwendet
     
%% Vordefinierte Werte für Variablen (Initialisierung)
Data.xLength = 1000; % length of plotted data = 1s
Data.numChannels = Data.kraftplot + Data.posplot + Data.geschwplot;
Data.yScale = [0 1 2]; % y scale of each normalized channel (1 = maximum)
Data.indYScale = 1;
Data.y = zeros(Data.xLength,2)+519;
Data.y(:,3) = 288;
Data.neueWerte = []; % Anzahl neuer Werte vom A/D speichern um Aufnahmefrequenz (im Nachhinein) adaptieren zu können
Data.geloescht = []; % Anzahl gelöschter Werte durch Befehl 'reshapeData'
Data.ynew = zeros(1000,3);
Data.hlkraftverlauf = line();
Data.xkraft = 0;
Data.tmp_reshaped = [];
set(Data.hpb_stop,'Enable','on'); 

    set(h,'UserData',Data); %...write actual data to main figure userdata
%end pb_load function
%==========

function pb_export(obj,event,h) % Export Button
    Data = get(h,'UserData'); %...read actual data from main figure userdata
    
    % Suche Speicherort aus und exportiere Daten dorthin
    OrigWerte = Data.y2;
    [filename pathname] = uiputfile('orig_data.mat', 'Save Data');
    save([pathname filename], 'OrigWerte');
   
    KalWerte = Data.kal;
    save([pathname 'kalibriert_data.mat'], 'KalWerte');
  
    set(h, 'UserData', Data);
%end pb_export function
%==========

function t1(obj,event,h) % ...called by timer 1
    Data = get(h,'UserData'); %...read actual data from main figure userdata
    
if Data.s.BytesAvailable > 0 % Falls Daten im Serial Port vorhanden sind...

    tmp = fread(Data.s,Data.s.BytesAvailable); % soviele Daten einlesen, wie vorhanden sind
    [tmp_reshapeData, Data.geloescht(end+1)] = reshapeData(tmp); % diese Funktion wandelt die empfangenen String-Daten in Zahlenwerte um
    tmp_reshapeData = tmp_reshapeData(:,1:3); % letzte Spalte (Trigger) löschen -> wird bisher noch nicht benötigt!! kann aber in Zukunft Verwendung finden!
    
    Data.tmp = tmp;
    
    Data.y = [Data.y; tmp_reshapeData]; % Neue Daten zu bisherigen hinzufügen

    Data.ynew = Data.y(end-999:end,:);
    Data.ynew(:,1) = Data.ynew(:,1).*1.04 - 552; % Drehmoment-Kalibration
    Data.ynew(:,2) = Data.ynew(:,2).*0.883 - 459.0; % Winkelgeschwindigkeit-Kalibration
    Data.ynew(:,3) = Data.ynew(:,3).*0.363 - 99.3; % Winkel-Kalibration
    Data.ylaenge = length(Data.y);
 
    [nr nc] = size(tmp_reshapeData);
    Data.neueWerte(end+1) = nr; % Anzahl neuer Werte speichern um Aufnahmefrequenz daran anzupassen
    Data.tmp_reshaped = [Data.tmp_reshaped; tmp_reshapeData];
end

% disp(Data.s.BytesAvailable); % Zeigt empfangene Bytes an
    
    set(h,'UserData',Data); %...write actual data to main figure userdata
%end t1 function

function tplot(obj,event,h) % ...called by timer 2
Data = get(h,'UserData'); %...read actual data from main figure userdata
delete(Data.handlekraftplot);

if length(Data.neueWerte) > 1 % Falls neue Werte vorhanden sind... (siehe Timer1 Funktion)
if Data.kraftplotkontrolle % Falls Checkbox "Kraftrampe" aktiviert wurde....
    set(Data.hl, 'XData', [], 'YData', []);
    if length(Data.tmp_reshaped) >= 1000
        stop(Data.ht1);
        stop(Data.htplot);
        return;
    end
    % Erstelle Beispiels Lines... MUSS NOCH VERBESSERT WERDEN!! Dient nur
    % als Beispiel
    line([1 400], [Data.y(1)-Data.y(1)*0.05 Data.y(1)-Data.y(1)*0.05]*500*1.04 - 552, 'Color', [1 1 1]);
    line([1 400], [Data.y(1)+Data.y(1)*0.05 Data.y(1)+Data.y(1)*0.05]*500*1.04 - 552, 'Color', [1 1 1]);
    line([400 800], [Data.y(1)-Data.y(1)*0.05 700-Data.y(1)*0.05]*500*1.04 - 552, 'Color', [1 1 1]);
    line([400 800], [Data.y(1)+Data.y(1)*0.05 700+Data.y(1)*0.05]*500*1.04 - 552, 'Color', [1 1 1]);
    line([800 1000], [700-Data.y(1)*0.05 700-Data.y(1)*0.05]*500*1.04 - 552, 'Color', [1 1 1]);
    line([800 1000], [700+Data.y(1)*0.05 700+Data.y(1)*0.05]*500*1.04 - 552, 'Color', [1 1 1]);
    
    
    line(1:length(Data.tmp_reshaped),Data.tmp_reshaped(:,1)*500*1.04 - 552,'Color', [0, 206/255, 209/255]);
    hold on;
    Data.handlekraftplot = plot(length(Data.tmp_reshaped),Data.tmp_reshaped(end,1)*500*1.04 - 552,'o','MarkerEdgeColor', 'y', 'MarkerFaceColor', 'r', 'MarkerSize', 10);
    set(Data.subplot1, 'YColor', [1 1 1], 'YLim', [2*10^5 4*10^5]);
    
    Data.xbeschriftung = 1000;
    
else % Falls alle Plots angezeigt werden sollen... aktualisiere jeweils die Lines in den Plots mit den neuen Daten
    for i=1:Data.numChannels,
        newdata = Data.ynew(:,i);
        set(Data.hl(i),'xData',1:Data.xLength);
        set(Data.hl(i),'YData',newdata-(i~=Data.numChannels)*mean(newdata)+(Data.numChannels-i)*Data.yScale(Data.indYScale)); % filling the plot with new data
        Data.xbeschriftung = floor(Data.ylaenge / 100) * 100;
    end
end
end


set(gca, 'XTickLabel', Data.xbeschriftung-1000:100:Data.xbeschriftung); % x-Achse soll jeweils 1000 Werte anzeigen

    set(h,'UserData',Data); %...write actual data to main figure userdata
%==========

function kraftverlauf(obj,event,h) % Checkbox für Kraftverlauf
Data = get(h,'UserData'); 
Data.kraftplot = get(Data.hcheckkraft, 'Value');
Data.numChannels = Data.kraftplot + Data.posplot + Data.geschwplot;

if Data.kraftplot && ~Data.posplot && ~Data.geschwplot
positionVector1 = [0.05, 0.25, 0.9, 0.6];
subplot('position', positionVector1, xlabel('Frames'), ylabel('Drehmoment [Nm]'));
plot(0,'Color', [0, 206/255, 209/255]);
set(gca,'color',[0 0 0], 'XColor', [1 1 1], xlabel('Frames'), ylabel('Drehmoment [Nm]'));  
else
subplots_darstellen(Data.xLength, Data.y, Data.numChannels, Data.yScale, Data.indYScale, 0); 
end

set(h,'UserData',Data); %...write actual data to main figure userdata
%==========

function posverlauf(obj,event,h) % Checkbox für Winkelverlauf
Data = get(h,'UserData'); 
Data.posplot = get(Data.hcheckpos, 'Value');
set(Data.hcheckgeschw, 'Value', Data.posplot);
if ~Data.posplot
    set(Data.hcheckkraft_kontrolle, 'Enable', 'on');
else
    set(Data.hcheckkraft_kontrolle, 'Enable', 'off');
end

Data.geschwplot = Data.posplot;

Data.numChannels = Data.kraftplot + Data.posplot + Data.geschwplot;

if Data.kraftplot && ~Data.posplot && ~Data.geschwplot
positionVector1 = [0.05, 0.25, 0.9, 0.6];
subplot('position', positionVector1);
plot(0,'Color', [0, 206/255, 209/255]);
set(gca,'color',[0 0 0], 'XColor', [1 1 1], 'YColor', [1 1 1]);  
else
subplots_darstellen(Data.xLength, Data.y, Data.numChannels, Data.yScale, Data.indYScale, 0); 
end

set(h,'UserData',Data); %...write actual data to main figure userdata
%==========

function geschwverlauf(obj,event,h) % Checkbox für Winkelgeschw.verlauf
Data = get(h,'UserData'); 
Data.geschwplot = get(Data.hcheckgeschw, 'Value');
set(Data.hcheckpos, 'Value', Data.geschwplot);
Data.posplot = Data.geschwplot;

if ~Data.posplot
    set(Data.hcheckkraft_kontrolle, 'Enable', 'on');
else
    set(Data.hcheckkraft_kontrolle, 'Enable', 'off');
end


Data.numChannels = Data.kraftplot + Data.posplot + Data.geschwplot;

if Data.kraftplot && ~Data.posplot && ~Data.geschwplot
    positionVector1 = [0.05, 0.25, 0.9, 0.6];
    subplot('position', positionVector1);
    plot(0,'Color', [0, 206/255, 209/255]);
    set(gca,'color',[0 0 0], 'XColor', [1 1 1], 'YColor', [1 1 1]);
else
    subplots_darstellen(Data.xLength, Data.y, Data.numChannels, Data.yScale, Data.indYScale, 0); 
end

set(h,'UserData',Data); %...write actual data to main figure userdata

function kraftkontrolle(obj,event,h) % Checkbox für Kraftrampe
Data = get(h,'UserData'); 
Data.kraftplotkontrolle = get(Data.hcheckkraft_kontrolle, 'Value');

if Data.kraftplotkontrolle
    set(Data.hcheckpos, 'Enable', 'off');
    set(Data.hcheckgeschw, 'Enable', 'off');
    set(Data.hcheckkraft, 'Enable', 'off');
else
    set(Data.hcheckpos, 'Enable', 'on');
    set(Data.hcheckgeschw, 'Enable', 'on');
    set(Data.hcheckkraft, 'Enable', 'on');
end

set(h,'UserData',Data); %...write actual data to main figure userdata
%==========