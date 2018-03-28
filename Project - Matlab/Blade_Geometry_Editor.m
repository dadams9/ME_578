%nrom:      Devin Adams & Mikkel Unrau
%nor:       Dr. Salmon
%Date:      25 April 2018
%Purpose:   This script reads in the starting dimensions for a compressor
%blade (the xy coordinates for a number of cross sections in the z
%direction) and then modifies the values to tweak the geometry a little and
%outputs the modified text file with the new blade dimensions.
clear all;
clc;


%%
% %Read in the DOE parameters assuming the DOE is on blade dimensions
% load('DOE_output.txt');
% DOE_num = DOE_output(:,1);
% numSecs = DOE_output(:,2);
% bChord = DOE_output(:,3);
% bTwist = DOE_output(:,4);
% bChordWOffset = DOE_output(:,5);
% bFAOffset = DOE_output(:,6);
% bHeight = DOE_output(:,7);
% 
%Dummy values for testing text file writing
% pchord = 29.2;
% ptwist = 19.1;
% pcwOff = 9.9;
% pafOff = 0.99;
% pheight = 109.9;

%Read in the DOE parameters assuming the DOE is on cross sectional
%percentages of a set blade dimension (twist, height, etc are fixed)
load('DOE_output_percentages.txt');
DOE_num = DOE_output_percentages(:,1);
numSecs = DOE_output_percentages(:,2);

%--------------------------------------------------------------------------
%--------------------------------------------------------------------------
%Assign the fixed blade parameters to each of the variables for output
%later. Comment this out if the blade dimensions are being altered.
for i=1:length(DOE_num)
    bChord(i) = 5;
    bTwist(i) = 30;
    bChordWOffset(i) = 1;
    bFAOffset(i) = 1;
    bHeight(i) = 5;
end
%--------------------------------------------------------------------------
%--------------------------------------------------------------------------

for i=1:length(numSecs)
    pchord(i,1:numSecs(i)) = DOE_output_percentages(i,3:7);
    ptwist(i,1:numSecs(i)) = DOE_output_percentages(i,8:12);
    pcwOff(i,1:numSecs(i)) = DOE_output_percentages(i,13:17);
    pafOff(i,1:numSecs(i)) = DOE_output_percentages(i,18:22);
    for j=1:numSecs(i)
        pheight(i,j) = (j-1)*bHeight(i)/(numSecs(i)-1);
    end
end
% pheight = DOE_output_percentages(:,7);




%Loop over each DOE. This loop reads in the base blade cross section file
%and uses that to create the new cross sections for the modified blade. It
%then writes a estg file to be used in STAR CCM and a text file for Spencer
%to use to create the blade in NX. kkk is the index used to keep track of
%which DOE design is being modified.
for kkk = 1:length(DOE_num)
    
    %Clear all variables that aren't being used and close the plots(if any)
    close all
    clearvars -except DOE_num numSecs bChord bChordWOffset bFAOffset bHeight bTwist kkk ...
               pchord ptwist pcwOff pafOff pheight
    

    
    %%
    %Read in compressor blade cross section geometry from txt file
    base_blade_filename = ['NacaM22.txt'];
    baseblade = load(base_blade_filename);
    
    %The ESTG file for Star CCM reads in the cross section points in order
    %from first to last points: the first third are all the Z coordinates,
    %the second third are all X coordinates, and the last third are all Y
    %coordinates for each airfoil cross section. Therefore, the first
    %airfoil cross section is flat and in the z plane and any additional
    %cross sections will be added later.
    %Air_Foil(section#; 1=Z, 2=X, 3=Y; coordinate value)
    Air_Foil(1,1,:) = zeros(length(baseblade(:,1)),1);
    Air_Foil(1,2,:) = baseblade(:,1);
    Air_Foil(1,3,:) = baseblade(:,2);
    
    %Copy of the original Airfoil geometry to manipulate later
    Air_Foil_Original = Air_Foil;
    
    %%
    % %--------------------------------------------------------------------------
    % %Read in the compressor blade geometry from an estg file.
    %
    % %Name of the file to read
    % fileID = fopen('base_blade.estg');
    %
    % %Read the file using spaces as the delimiter and skipping the first 8
    % %lines. There are 135 columns for each airfoil
    % % base_blade = dlmread(in_filename, ' ',8,0);
    % base_blade_cell = textscan(fileID,'%n',...
    %                              'Delimiter','  ',...
    %                              'CommentStyle','AIRFOIL',...
    %                              'HeaderLines',12);
    % fclose(fileID);
    %
    % %Convert the cell to an array of doubles
    % base_blade_doubles = (base_blade_cell{1,1});
    %
    % %Check to see where the array is NaN
    % TF = isnan(base_blade_doubles);
    %
    % %Filter out any NaN values in the array to get just one long array of zxy
    % %coordinates. The format of the array is as follows:
    % %Airfoil 1: First 135 elements are the Z coordinates
    % %           Second 135 elements are the X coordinates
    % %           Third 135 elements are the Y coordinates
    % %Airfoil 2: Fourth 135 elements are the Z coordinates and so on.
    % j=1;
    % for i=1:length(base_blade_doubles)
    %     if TF(i) == 0
    %         base_blade_ZXY_dimensions(j) = base_blade_doubles(i);
    %         j=j+1;
    %     end
    % end
    %
    %
    % %Separate the base blade dimensions into individual airfoil XYZ
    % %coordinates
    % counter = 1;
    % for i=1:23
    %     for j=1:3
    %         for k=1:135
    % %             if j==1 && k==1
    % %                 Air_Foil(i,j,k) = base_blade_ZXY_dimensions(counter);
    % %             elseif j==1
    % %                 Air_Foil(i,j,k) = Air_Foil(i,j,1);
    % %             else
    %                 Air_Foil(i,j,k) = base_blade_ZXY_dimensions(counter);
    % %             end
    %             counter=counter+1;
    %         end
    %     end
    % end
    % %--------------------------------------------------------------------------
    %%
    %Modify the blade cross sections
    
    %Define each section to be identical to the baseblade cross section -
    %(Air_Foil(1,:,:)
    if numSecs(kkk) > 1
    for nnn = 1:numSecs(kkk)
        for i=1:length(Air_Foil(1,1,:))
            Air_Foil(nnn,1,i) = pheight(kkk,nnn);
            Air_Foil(nnn,2,i) = Air_Foil(1,2,i);
            Air_Foil(nnn,3,i) = Air_Foil(1,3,i);
        end
    end
    end
    
    %Do rotations and stuff to the actual blade cross section------------------
    %Rotate each point about the leading edge on the origin (0,0) based on
    %percentage of twist for the give cross section. 
    if numSecs(kkk) > 1
    for nnn = 1:numSecs(kkk)
        %Define cos and sin values for the transformation
        c = cosd(bTwist(kkk)*ptwist(kkk,nnn));
        s = sind(bTwist(kkk)*ptwist(kkk,nnn));
        
        %Define the chord transformation
        chord_trans = bChord(kkk)*pchord(kkk,nnn);
        
        %Define the chord offset transformation
        chord_off_trans = bChordWOffset(kkk)*pcwOff(kkk,nnn);
        
        %Define the AF offset transformation
        af_off_trans = bFAOffset(kkk)*pafOff(kkk,nnn);
        
        %Apply all transformations to each point for each cross section
        for i=1:length(Air_Foil(1,1,:))
            Air_Foil(nnn,1,i) = pheight(kkk,nnn);
            Air_Foil(nnn,2,i) = ((Air_Foil_Original(1,2,i)*c-Air_Foil_Original(1,3,i)*s)*chord_trans)+chord_off_trans;
            Air_Foil(nnn,3,i) = ((Air_Foil_Original(1,2,i)*s+Air_Foil_Original(1,3,i)*c)*chord_trans)+af_off_trans;
        end
    end
    end
    

    
    %--------------------------------------------------------------------------
    
    %Plot the cross sections to see if they were made correctly
    for i=1:length(Air_Foil(:,1,1))
        for j=1:length(baseblade)
            x(j) = Air_Foil(i,2,j);
            y(j) = Air_Foil(i,3,j);
            z(j) = Air_Foil(i,1,j);
        end
        figure(2)
        plot3(x,y,z,'k')
        hold on
    end
    %%
    %Write new blade ESTG file for Star CCM
    
    %Name of the file to output
    outputfile = ['new_blade_',num2str(DOE_num(kkk)),'.estg'];
    
    %Output all of the desired information into an estg file formatted the same
    %as the base blade file
    fID = fopen(outputfile,'w');
    fprintf(fID, '# nstart %1i\n', 3);
    fprintf(fID, '# nspt %1i\n', 36);
    fprintf(fID, '# iang %1i\n', 0);
    fprintf(fID, '# nsl %1i\n', 11);
    fprintf(fID, '# icamb %1i\n', 1);
    fprintf(fID, '# nlept %1i\n', 23);
    fprintf(fID, '# ntept %1i\n', 23);
    fprintf(fID, '# stackx %4.4f\n', -7.0355);
    fprintf(fID, '# nblade %1i\n', 20);
    fprintf(fID, '# isurf %1i\n', 1);
    fprintf(fID, 'NPTS %1i\n', length(Air_Foil(1,1,:)));
    for i=1:length(Air_Foil(:,1,1))
        fprintf(fID, 'AIRFOIL %1i\n ',i);
        for j=1:3
            for k=1:length(baseblade)
                if j==1
                    fprintf(fID,'%4.11f  ',Air_Foil(i,j,k));
                else
                    fprintf(fID,'%4.11f   ',Air_Foil(i,j,k));
                end
            end
            fprintf(fID, '\n  ');
        end
        fprintf(fID, '\n');
    end
    
    fclose(fID);
    
    %%
    %Write the NX input file for the new blade
    NX_filename = ['NX_new_blade', num2str(DOE_num(kkk)),'.txt'];
    fid = fopen(NX_filename,'w');
    fprintf(fid,'SaveNum %i\n', DOE_num(kkk));
    fprintf(fid, 'numSecs %i\n', numSecs(kkk));
    fprintf(fid, 'bChord %4.4f\n', bChord(kkk));
    fprintf(fid, 'bTwist %i\n', bTwist(kkk));
    fprintf(fid, 'bChordWOffset %4.4f\n', bChordWOffset(kkk));
    fprintf(fid, 'bFAOffset %4.4f\n', bFAOffset(kkk));
    fprintf(fid, 'bHeight %4.4f\n', bHeight(kkk));
    for i = 1:numSecs(kkk)
        fprintf(fid, 'affileName\t%s \n',base_blade_filename);
        fprintf(fid, 'pchord\t%4.4f\n',pchord(kkk,i));
        fprintf(fid, 'ptwist\t%4.4f\n',ptwist(kkk,i));
        fprintf(fid, 'pcwOff\t%4.4f\n',pcwOff(kkk,i));
        fprintf(fid, 'pafOff\t%4.4f\n',pafOff(kkk,i));
        fprintf(fid, 'pheight\t%4.4f\n',pheight(kkk,i)/bHeight(kkk));
    end
    fclose(fid);
    
end