function hl = subplots_darstellen(xLength, y, numChannels, yScale, indYscale, option)

% option: 1 | 0 = mit oder ohne Lines

if nargin == 5
    option = 1;
end

    positionVector1 = [0.05, 0.65, 0.7, 0.2];
    subplot('position', positionVector1);
    plot(0,'Color', [0, 206/255, 209/255]);
    set(gca,'color',[0 0 0], 'XColor', [1 1 1],'XTick', [], 'YColor', [1 1 1]); 
    ylabel('Drehmoment [Nm]');
    if option == 1
    hl(1) = line(1:xLength,y(end-999:end,1)+(numChannels-1)*yScale(indYscale),'Color', [0, 206/255, 209/255]);
    end    
    
    
    positionVector2 = [0.05, 0.45, 0.7, 0.2];
    subplot('position', positionVector2)
    set(gca,'Color','white')
    plot(0,'Color', [138/255,43/255,226/255]);
    set(gca,'color',[0 0 0], 'XColor', [1 1 1],'XTick', [], 'YColor', [1 1 1]);
    ylabel('W.-Geschw. [°/s]');
    if option == 1
    hl(2) = line(1:xLength,y(end-999:end,2)+(numChannels-2)*yScale(indYscale),'Color', [138/255,43/255,226/255]);
    end
    
    positionVector3 = [0.05, 0.25, 0.7, 0.2];
    subplot('position', positionVector3)
    set(gca,'Color','white')
    plot(0,'Color', [0/255,191/255,255/255]);
    set(gca,'color',[0 0 0], 'XColor', [1 1 1], 'YColor', [1 1 1]);
    ylabel('Winkel [°]');
    xlabel('Frames');
    if option == 1
    hl(3) = line(1:xLength,y(end-999:end,3)+(numChannels-3)*yScale(indYscale), 'Color', [135/255,206/255,250/255]);
    end