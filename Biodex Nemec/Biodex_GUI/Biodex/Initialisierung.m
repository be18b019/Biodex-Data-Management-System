
Data.xLength = 1000; % length of plotted data = 1s
Data.numChannels = Data.kraftplot + Data.posplot + Data.geschwplot;
Data.yScale = [0 1 2]; % y scale of each normalized channel (1 = maximum)
Data.indYScale = 1;
Data.y = zeros(Data.xLength,2)+442;
Data.y(:,3) = 288;
Data.neueWerte = []; % Anzahl neuer Werte vom A/D speichern um Aufnahmefrequenz (im Nachhinein) adaptieren zu können
Data.geloescht = []; % Anzahl gelöschter Werte durch Befehl 'reshapeData'
Data.ynew = zeros(1000,3);
Data.hlkraftverlauf = line();
Data.xkraft = 0;
Data.tmp_reshaped = [];
Data.ylaenge = 1;
Data.drehmom = [];

subplots_darstellen(Data.xLength, Data.y, Data.numChannels, Data.yScale, Data.indYScale, 0);