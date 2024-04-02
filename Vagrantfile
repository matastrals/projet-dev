Vagrant.configure("2") do |config|
    config.vm.define "web1" do |web1|
        web1.vm.box = "generic/rocky9"
        web1.vm.network "private_network", type: "dhcp"
        web1.vm.network "private_network", type: "static", ip: "10.5.1.11"
        web1.vm.hostname = "web1.tp5.b2"
        web1.vm.provision "shell", path: "scripts/configure_web1.sh"
        web1.vm.synced_folder ".", "/vagrant", disabled: false
        web1.vm.provider "virtualbox" do |vb|
          vb.memory = 512
        end
      end
      config.vm.define "db1" do |db1|
        db1.vm.box = "generic/rocky9"
        db1.vm.network "private_network", type: "dhcp"
        db1.vm.network "private_network", type: "static", ip: "10.5.1.211"
        db1.vm.hostname = "db1.tp5.b2"
        db1.vm.provision "shell", path: "scripts/configure_db1.sh"
        db1.vm.synced_folder ".", "/vagrant", disabled: true 
        db1.vm.provider "virtualbox" do |vb|
          vb.memory = 512
        end
      end
    end