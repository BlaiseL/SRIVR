#include <ros/ros.h>
#include <stdio.h>
#include <octomap/octomap.h>
#include <octomap/OcTree.h>
#include <octomap/OcTreeBase.h>
#include "pcl/io/pcd_io.h"
#include "pcl/point_types.h"
#include "converter.h"
#include <iostream>
#include <fstream>
#include <octomap/AbstractOcTree.h>
#include <octomap/AbstractOccupancyOcTree.h>
#include <math.h>
#include <fstream>

using namespace std;
using namespace octomap;


void Converter::pcd2obj(const std::string& inputFilename, const std::string& outputFilename)
{
    pcl::PointCloud<pcl::PointXYZ> cloud;

    if (pcl::io::loadPCDFile<pcl::PointXYZ> (inputFilename, cloud) == -1)
    {
        std::cerr << "Couldn't read file " << inputFilename << std::endl;
        return;
    }

    const size_t size = cloud.points.size();
    std::ofstream os(outputFilename.c_str());

    for(unsigned int i=0 ; i<size ; i++)
    {
        // Remove nan
        if(!std::isnan(cloud.points[i].x))
        {
            os << "v ";
            os << cloud.points[i].x << " ";
            os << cloud.points[i].y << " ";
            os << cloud.points[i].z << "\n";
        }
    }

    os.close();
}


void Converter::obj2pcd(const std::string& inputFilename, const std::string& outputFilename)
{
    pcl::PointCloud<pcl::PointXYZ> cloud;

    // Input stream
    std::ifstream is(inputFilename.c_str());

    // Read line by line
    for(std::string line; std::getline(is, line); )
    {
        std::istringstream in(line);

        std::string v;
        in >> v;
        if (v != "v") continue;

        // Read x y z
        float x, y, z;
        in >> x >> y >> z;
        cloud.push_back(pcl::PointXYZ(x, y, z));
    }

    is.close();

    // Save to pcd file
    pcl::io::savePCDFileBinaryCompressed(outputFilename, cloud);
}
int main(int argc, char* argv[])
{Converter::pcd2obj("/home/zhuxy/catkin_ws/src/pcd/src/file.pcd", "pcd2obj.obj");
}
