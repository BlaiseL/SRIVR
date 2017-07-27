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





int main(int argc, char* argv[])
{
   // This must be called before anything else ROS-related
  ros::init(argc, argv, "pcd");

  // Create a ROS node handle
    ros::NodeHandle node;
string file ="/home/zhuxy/catkin_ws/src/pcd/src/map.ot";
  int i=0;
  pcl::PointCloud<pcl::PointXYZ> cloud; 
  AbstractOcTree* tree = AbstractOcTree::read(file);
  OcTree* octree = dynamic_cast<OcTree*>(tree);
  if (tree!=NULL){
    ROS_INFO("Not NULL");
  }
//  cloud = new pcl::PointCloud<pcl::PointXYZ>();
 int len= int(sqrt(octree->calcNumNodes()));
 cloud.width  = len;
  cloud.height = len;
  cloud.points.resize (cloud.width * cloud.height);

  //cloud.points.resize (octree->calcNumNodes());
      for(OcTree::leaf_iterator it = octree->begin_leafs(),
          end=octree->end_leafs(); it!= end; ++it, i++)
  {
    //add point in point cloud
  //ROS_INFO("Hello, World!");
  //  string ret= string(it.getX());
//    ROS_INFO("%.15g \n", it.getX()) ;
    //std::cout << "Node center: " << it.getX() << std::endl;
    
              cloud.points[i].x = it.getX();
              cloud.points[i].y = it.getY();
               cloud.points[i].z = it.getZ();  
            //    ROS_INFO("Hello, World!");

  }
 ROS_INFO("Hello, World!");

pcl::io::savePCDFileASCII ("/home/zhuxy/catkin_ws/src/pcd/src/file.pcd", cloud);

}
